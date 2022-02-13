using GitHub.Repository.Analyzer.GitHub.Client.Client;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using Microsoft.Extensions.Logging;

namespace GitHub.Repository.Analyzer.Loader.ClientProvider
{
  public class GitHubClientProvider : IGitHubClientProvider
  {
    private readonly IGitHubClientBuilder _gitHubClientBuilder;
    private readonly ILogger<GitHubClientProvider> _logger;

    public GitHubClientProvider(IGitHubClientBuilder gitHubClientBuilder, ILogger<GitHubClientProvider> logger)
    {
      _gitHubClientBuilder = gitHubClientBuilder;
      _logger = logger;
    }

    public IGitHubClient GetClient(GitHubClientData clientData)
    {
      _logger.LogDebug($"Getting GitHub client from {nameof(GitHubClientProvider)}");

      return _gitHubClientBuilder.Build(clientData);
    }
  }
}
