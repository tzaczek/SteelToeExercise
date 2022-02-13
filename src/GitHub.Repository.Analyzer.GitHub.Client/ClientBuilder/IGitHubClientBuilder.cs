using GitHub.Repository.Analyzer.GitHub.Client.Client;

namespace GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder
{
  public interface IGitHubClientBuilder
  {
    IGitHubClient Build(GitHubClientData clientData);
  }
}