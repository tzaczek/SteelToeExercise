using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository.Analyzer.Processor.Communication;

namespace GitHub.Repository.Analyzer.Processor.Client.Client
{
  public class GrpcLicenseProcessorClient : ILicenseProcessorClient
  {
    private readonly ProcessRepositoryLicenseServiceDefintion.ProcessRepositoryLicenseServiceDefintionClient _client;
    private readonly IMapper _mapper;

    internal GrpcLicenseProcessorClient(
      ProcessRepositoryLicenseServiceDefintion.ProcessRepositoryLicenseServiceDefintionClient client,
      IMapper mapper)
    {
      _client = client;
      _mapper = mapper;
    }

    public async Task<ProcessRepositoryLicenseResult> Process(ProcessRepositoryLicenseData processRepositoryLicenseData, CancellationToken cancellationToken)
    {
      Guard.Against.Null(processRepositoryLicenseData, nameof(processRepositoryLicenseData));

      var gRpcRequest = _mapper.Map<ProcessRepositoryLicenseRequest>(processRepositoryLicenseData);

      var gRpcResponse = await _client.ProcessLicenseAsync(gRpcRequest, cancellationToken: cancellationToken);

      return _mapper.Map<ProcessRepositoryLicenseResult>(gRpcResponse);
    }
  }
}
