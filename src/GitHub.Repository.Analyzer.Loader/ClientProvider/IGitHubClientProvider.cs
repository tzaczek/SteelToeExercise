using GitHub.Repository.Analyzer.GitHub.Client.Client;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;

namespace GitHub.Repository.Analyzer.Loader.ClientProvider
{
  public interface IGitHubClientProvider
  {
    IGitHubClient GetClient(GitHubClientData clientData);
  }
}