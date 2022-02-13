using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using Octokit;

namespace GitHub.Repository.Analyzer.GitHub.Client.Client
{
  public class DefaultGitHubClient : IGitHubClient
  {
    private readonly GitHubClient _client;
    private readonly IMapper _mapper;

    internal DefaultGitHubClient(GitHubClient client, IMapper mapper)
    {
      _client = client;
      _mapper = mapper;
    }

    public async Task<IList<GitHubRepository>> GetAllRepositoriesForCurrent()
    {
      var gitHubRepositories = await _client.Repository.GetAllForCurrent();

      return gitHubRepositories.Select(y => _mapper.Map<GitHubRepository>(y)).ToList();
    }

    public async Task<IList<GitHubRepository>> GetAllStarredRepositoriesForCurrent()
    {
      var gitHubRepositories = await _client.Activity.Starring.GetAllForCurrent();

      return gitHubRepositories.Select(y => _mapper.Map<GitHubRepository>(y)).ToList();
    }
  }
}
