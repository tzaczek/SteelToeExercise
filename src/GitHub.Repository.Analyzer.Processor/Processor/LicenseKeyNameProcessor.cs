using System;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.Processor.Communication;
using Microsoft.Extensions.Logging;

namespace GitHub.Repository.Analyzer.Processor.Processor
{
  public class LicenseKeyNameProcessor : IRepositoryProcessor
  {
    private readonly ILogger<LicenseKeyNameProcessor> _logger;

    public LicenseKeyNameProcessor(ILogger<LicenseKeyNameProcessor> logger)
    {
      _logger = logger;
    }

    public ProcessorType ProcessorType => ProcessorType.LicenseKeyNameProcessor;

    public async Task<ProcessRepositoryLicenseReply> Process(ProcessRepositoryLicenseRequest request)
    {
      _logger.LogDebug($"{nameof(LicenseKeyNameProcessor)} process request for repository {request.RepositoryName}");

      //simulate long lasting operation
      await Task.Delay(TimeSpan.FromSeconds(1));

      var result = request.LicenseKey.Contains(request.LicenseKeySearchDefinition, StringComparison.InvariantCultureIgnoreCase);

      return new ProcessRepositoryLicenseReply
      {
        Message = result ? $"License matches {request.LicenseKeySearchDefinition}" : "License does not match",
        ProcessingResult = result,
        RepoistoryName = request.RepositoryName
      };
    }
  }
}
