using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GitHub.Repository.Analyzer.Processor.Communication;
using GitHub.Repository.Analyzer.Processor.Processor;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GitHub.Repository.Analyzer.Processor.Service
{
  public class ProcessRepositoryLicenseService 
    : ProcessRepositoryLicenseServiceDefintion.ProcessRepositoryLicenseServiceDefintionBase

  {
    private readonly ILogger<ProcessRepositoryLicenseService> _logger;
    private readonly IDictionary<ProcessorType, IRepositoryProcessor> _repositoryProcessors;

    public ProcessRepositoryLicenseService(
      ILogger<ProcessRepositoryLicenseService> logger,
      IEnumerable<IRepositoryProcessor> repositoryProcessors)
    {
      _logger = logger;
      _repositoryProcessors = repositoryProcessors.ToDictionary(y => y.ProcessorType, y => y);
    }

    public override Task<ProcessRepositoryLicenseReply> ProcessLicense(ProcessRepositoryLicenseRequest request, ServerCallContext context)
    {
      _logger.LogDebug($"{nameof(ProcessLicense)} request");

      Guard.Against.Null(request, nameof(request));

      const ProcessorType compatibleProcessorType = ProcessorType.LicenseKeyNameProcessor;

      if (!_repositoryProcessors.TryGetValue(compatibleProcessorType, out var processor))
      {
        throw new ApplicationException(
          $"Required by {nameof(ProcessLicense)} request, processor of type {compatibleProcessorType} is not registered");
      }

      return processor.Process(request);
    }
  }
}