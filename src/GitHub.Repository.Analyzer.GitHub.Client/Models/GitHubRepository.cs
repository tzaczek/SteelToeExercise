namespace GitHub.Repository.Analyzer.GitHub.Client.Models
{
  public record GitHubRepository
  {
    public string Id { get; init; }
    public string Name { get; init; }
    public string Url { get; init; }
    public GitHubLicense License { get; init; }
  }
}
