using AutoMapper;
using GitHub.Repository.Analyzer.Processor.Client.Client;
using GitHub.Repository.Analyzer.Processor.Communication;
using Grpc.Net.Client;

namespace GitHub.Repository.Analyzer.Processor.Client.ClientBuilder
{
  public class GrpcLicenseProcessorClientBuilder : ILicenseProcessorClientBuilder
  {
    private readonly IMapper _mapper;

    public GrpcLicenseProcessorClientBuilder(IMapper mapper)
    {
      _mapper = mapper;
    }

    public ILicenseProcessorClient Build(LicenseProcessorClientData clientData)
    { 
      var channel = GrpcChannel.ForAddress(clientData.Url);
      var gRpcClient = new ProcessRepositoryLicenseServiceDefintion.ProcessRepositoryLicenseServiceDefintionClient(channel);

      return new GrpcLicenseProcessorClient(gRpcClient, _mapper);
    }
  }
}
