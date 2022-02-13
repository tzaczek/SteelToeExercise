using GitHub.Repository.Analyzer.Processor.Client.Client;

namespace GitHub.Repository.Analyzer.Processor.Client.ClientBuilder
{
  public interface ILicenseProcessorClientBuilder
  {
    ILicenseProcessorClient Build(LicenseProcessorClientData clientData);
  }
}