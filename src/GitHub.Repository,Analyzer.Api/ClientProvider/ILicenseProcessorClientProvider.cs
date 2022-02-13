using GitHub.Repository.Analyzer.Processor.Client.Client;

namespace GitHub.Repository_Analyzer.Api.ClientProvider
{
  public interface ILicenseProcessorClientProvider
  {
    ILicenseProcessorClient GetClient();
  }
}