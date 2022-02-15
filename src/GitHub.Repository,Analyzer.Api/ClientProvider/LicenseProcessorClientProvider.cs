using GitHub.Repository.Analyzer.Processor.Client.Client;
using GitHub.Repository.Analyzer.Processor.Client.ClientBuilder;
using GitHub.Repository_Analyzer.Api.Configuration;
using Microsoft.Extensions.Options;

namespace GitHub.Repository_Analyzer.Api.ClientProvider
{
  public class LicenseProcessorClientProvider : ILicenseProcessorClientProvider
  {
    private readonly ILicenseProcessorClientBuilder _licenseProcessorClientBuilder;
    private readonly IOptions<LicenseProcessorClientConfiguration> _licenseProcessorClientSettings;

    public LicenseProcessorClientProvider(
      ILicenseProcessorClientBuilder licenseProcessorClientBuilder, 
      IOptions<LicenseProcessorClientConfiguration> licenseProcessorClientSettings)
    {
      _licenseProcessorClientBuilder = licenseProcessorClientBuilder;
      _licenseProcessorClientSettings = licenseProcessorClientSettings;
    }

    public ILicenseProcessorClient GetClient()
    {
      return _licenseProcessorClientBuilder.Build(
        new LicenseProcessorClientData { Url = _licenseProcessorClientSettings.Value.LicenseProcessorAddress });
    }
  }
}
