using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository_Analyzer.Api.Cache;
using GitHub.Repository_Analyzer.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace GitHub.Repository_Analyzer.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RepositoryAnalyzerController : ControllerBase
  {
    private readonly ILogger<RepositoryAnalyzerController> _logger;
    private readonly GitHubRepositoryLoaderService _gitHubRepositoryLoaderService;
    private readonly GitHubRepositoryLicenseProcessorService _gitHubRepositoryLicenseProcessorService;
    private readonly ICacheStorage _cacheStorage;

    public RepositoryAnalyzerController(
      ILogger<RepositoryAnalyzerController> logger, 
      GitHubRepositoryLoaderService gitHubRepositoryLoaderService,
      GitHubRepositoryLicenseProcessorService gitHubRepositoryLicenseProcessorService, 
      ICacheStorage cacheStorage)
    {
      _logger = logger;
      _gitHubRepositoryLoaderService = gitHubRepositoryLoaderService;
      _gitHubRepositoryLicenseProcessorService = gitHubRepositoryLicenseProcessorService;
      _cacheStorage = cacheStorage;
    }

    /// <summary>
    /// Retrieve starred repositories having specified license type
    /// </summary>
    /// <param name="accessToken">Private access token for accessing GitHub API</param>
    /// <param name="licenseName">The name of the license used to filter starred repositories</param>
    /// <param name="useCache">Use cache for given accessToken and licenseName</param>
    /// <returns></returns>
    [HttpGet(Name = "GetStarredRepositoriesHavingLicense")]
    [SwaggerOperation(
      Summary = "Get starred repositories with a matching license",
      Description = "Retrieve starred repositories having specified license type",
      OperationId = "GetStarredRepositoriesHavingLicense",
      Tags = new[] { "Starred-Repositories" })]
    public async Task<ActionResult<IList<ProcessRepositoryLicenseResult>>> Get(string accessToken, string licenseName, bool useCache = true)
    {
      _logger.LogDebug("GetStarredRepositoriesHavingLicense");

      //ToDo Caching will be moved to custom caching middleware 

      var cacheKey = CacheKeyBuilder.Build(new List<object>{accessToken, licenseName});

      if (useCache)
      {
        var cachedResults = await _cacheStorage.GetCacheItem<IList<ProcessRepositoryLicenseResult>>(cacheKey);

        if (cachedResults != null)
        {
          return Ok(cachedResults);
        }
      }
      else
      {
        await _cacheStorage.RemoveCacheItem(cacheKey);
      }

      IList<GitHubRepository> loadedRepositories;

      try
      {
        loadedRepositories = await _gitHubRepositoryLoaderService.LoadStarredRepositories(
          new GitHubClientData
          {
            AccessToken = accessToken,
            OrganizationName = "RepositoryAnalyzerAPI"
          });
      }
      catch (Exception e)
      {
        return Problem(e.ToString(), statusCode: 500, title: "Error while loading repositories from GitHub");
      }
      
      if (loadedRepositories == null || !loadedRepositories.Any())
      {
        return NotFound("Starred repositories for current user");
      }

      //Fan-out processing requests

      var matchingStarredRepositories = await _gitHubRepositoryLicenseProcessorService.Process(
        loadedRepositories,
        licenseName,
        CancellationToken.None);

      if (matchingStarredRepositories.Any() && useCache)
      {
        await _cacheStorage.SetCacheItem(cacheKey, matchingStarredRepositories);
      }

      return Ok(matchingStarredRepositories);
    }
  }
}