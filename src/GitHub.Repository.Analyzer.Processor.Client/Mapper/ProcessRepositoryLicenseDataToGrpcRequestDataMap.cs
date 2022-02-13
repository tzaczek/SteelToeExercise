using AutoMapper;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository.Analyzer.Processor.Communication;

namespace GitHub.Repository.Analyzer.Processor.Client.Mapper
{
  public class ProcessRepositoryLicenseDataToGrpcRequestDataMap : Profile
  {
    public ProcessRepositoryLicenseDataToGrpcRequestDataMap()
    {
      CreateMap<ProcessRepositoryLicenseData, ProcessRepositoryLicenseRequest>()
        .ForMember(
          d => d.LicenseKey,
          s => s.MapFrom(y => y.LicenseKey))
        .ForMember(
          d => d.LicenseKeySearchDefinition,
          s => s.MapFrom(y => y.LicenseKeySearchDefinition))
        .ForMember(
          d => d.LicenseName,
          s => s.MapFrom(y => y.LicenseName))
        .ForMember(
          d => d.RepositoryName,
          s => s.MapFrom(y => y.RepositoryName));
    }
  }
}