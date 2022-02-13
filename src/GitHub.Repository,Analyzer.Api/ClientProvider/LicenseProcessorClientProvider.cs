using GitHub.Repository.Analyzer.Processor.Client.Client;
using GitHub.Repository.Analyzer.Processor.Client.ClientBuilder;

namespace GitHub.Repository_Analyzer.Api.ClientProvider
{
  public class LicenseProcessorClientProvider : ILicenseProcessorClientProvider
  {
    private readonly ILicenseProcessorClientBuilder _licenseProcessorClientBuilder;

    public LicenseProcessorClientProvider(ILicenseProcessorClientBuilder licenseProcessorClientBuilder)
    {
      _licenseProcessorClientBuilder = licenseProcessorClientBuilder;
    }

    public ILicenseProcessorClient GetClient()
    {
      return _licenseProcessorClientBuilder.Build(new LicenseProcessorClientData { Url = "http://localhost:5004" });
    }
  }
}
