namespace GitHub.Repository.Analyzer.Processor.Client.Models
{
  public record ProcessRepositoryLicenseData
  {
    public string RepositoryName { get; init; }
    public string LicenseKey { get; init; }
    public string LicenseName { get; init; }
    public string LicenseKeySearchDefinition { get; init; }
  }
}
