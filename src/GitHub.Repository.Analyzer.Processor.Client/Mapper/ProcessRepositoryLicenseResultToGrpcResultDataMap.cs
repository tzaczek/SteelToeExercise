using AutoMapper;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository.Analyzer.Processor.Communication;

namespace GitHub.Repository.Analyzer.Processor.Client.Mapper
{
  public class ProcessRepositoryLicenseResultToGrpcResultDataMap : Profile
  {
    public ProcessRepositoryLicenseResultToGrpcResultDataMap()
    {
      CreateMap<ProcessRepositoryLicenseReply, ProcessRepositoryLicenseResult>()
        .ForMember(
          d => d.ResultDescription,
          s => s.MapFrom(y => y.Message))
        .ForMember(
          d => d.Result,
          s => s.MapFrom(y => y.ProcessingResult))
        .ForMember(
          d => d.RepositoryName,
          s => s.MapFrom(y => y.RepoistoryName));
    }
  }
}