namespace GitHub.Repository.Analyzer.Processor.Client.Models
{
  public record ProcessRepositoryLicenseResult
  {
    public bool Result { get; init; }
    public string ResultDescription { get; init; }
    public string RepositoryName { get; set; }
  }
}
