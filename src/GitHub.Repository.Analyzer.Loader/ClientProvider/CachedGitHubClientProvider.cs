using System.Collections.Concurrent;
using GitHub.Repository.Analyzer.GitHub.Client.Client;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using Microsoft.Extensions.Logging;

namespace GitHub.Repository.Analyzer.Loader.ClientProvider
{
  public class CachedGitHubClientProvider : IGitHubClientProvider
  {
    private readonly IGitHubClientProvider _target;
    private readonly ConcurrentDictionary<GitHubClientData, IGitHubClient> _cache;
    private readonly ILogger<CachedGitHubClientProvider> _logger;

    public CachedGitHubClientProvider(IGitHubClientProvider target, ILogger<CachedGitHubClientProvider> logger)
    {
      _target = target;
      _logger = logger;

      _cache = new ConcurrentDictionary<GitHubClientData, IGitHubClient>();
    }

    public IGitHubClient GetClient(GitHubClientData clientData)
    {
      _logger.LogDebug($"Getting GitHub client from {nameof(CachedGitHubClientProvider)}");

      return _cache.GetOrAdd(clientData, data => _target.GetClient(data));
    }
  }
}
