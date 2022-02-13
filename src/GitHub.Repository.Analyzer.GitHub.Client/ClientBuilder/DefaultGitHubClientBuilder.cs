using AutoMapper;
using GitHub.Repository.Analyzer.GitHub.Client.Client;
using Octokit;
using IGitHubClient = GitHub.Repository.Analyzer.GitHub.Client.Client.IGitHubClient;

namespace GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder
{
  public class DefaultGitHubClientBuilder : IGitHubClientBuilder
  {
    private readonly IMapper _mapper;

    public DefaultGitHubClientBuilder(IMapper mapper)
    {
      _mapper = mapper;
    }

    public IGitHubClient Build(GitHubClientData clientData)
    {
      var tokenAuth = new Credentials(clientData.AccessToken);

      var client = new GitHubClient(new ProductHeaderValue(clientData.OrganizationName))
      {
        Credentials = tokenAuth
      };

      return new DefaultGitHubClient(client, _mapper);
    }
  }
}
