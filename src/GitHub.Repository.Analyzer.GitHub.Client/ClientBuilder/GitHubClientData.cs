namespace GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder
{
  public record GitHubClientData
  {
    public string AccessToken { get; init; }
    public string OrganizationName { get; init; }
  }
}
