using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.GitHub.Client.Models;

namespace GitHub.Repository.Analyzer.GitHub.Client.Client
{
  public interface IGitHubClient
  {
    Task<IList<GitHubRepository>> GetAllRepositoriesForCurrent();
    Task<IList<GitHubRepository>> GetAllStarredRepositoriesForCurrent();
  }
}