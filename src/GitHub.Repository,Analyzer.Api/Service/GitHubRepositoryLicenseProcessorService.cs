using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository_Analyzer.Api.ClientProvider;
using Microsoft.Extensions.Logging;

namespace GitHub.Repository_Analyzer.Api.Service
{
  public class GitHubRepositoryLicenseProcessorService
  {
    private readonly ILogger<GitHubRepositoryLicenseProcessorService> _logger;
    private readonly ILicenseProcessorClientProvider _licenseProcessorClientProvider;

    public GitHubRepositoryLicenseProcessorService(
      ILogger<GitHubRepositoryLicenseProcessorService> logger, ILicenseProcessorClientProvider licenseProcessorClientProvider)
    {
      _logger = logger;
      _licenseProcessorClientProvider = licenseProcessorClientProvider;
    }

    public async Task<IList<ProcessRepositoryLicenseResult>> Process(IList<GitHubRepository> repositories, string licenseName, CancellationToken cancellationToken)
    {
      Guard.Against.Null(repositories, nameof(repositories));
      Guard.Against.Null(licenseName, nameof(licenseName));

      _logger.LogDebug("Process license");

      var client = _licenseProcessorClientProvider.GetClient();

      var results = await Task.WhenAll(repositories.Select(repository =>
        client.Process(new ProcessRepositoryLicenseData
        {
          LicenseKeySearchDefinition = licenseName,
          LicenseKey = repository.License?.Key,
          LicenseName = repository.License?.Name,
          RepositoryName = repository.Name
        }, cancellationToken))
      );

      _logger.LogDebug($"Processed {results.Length} repositories");

      return results.Where(y => y is { Result: true }).ToList();
    }
  }
}
