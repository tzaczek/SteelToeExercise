using AutoMapper;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using Octokit;

namespace GitHub.Repository.Analyzer.GitHub.Client.Mapper
{
  public class OctokitLicenseToModelLicenseMap : Profile
  {
    public OctokitLicenseToModelLicenseMap()
    {
      CreateMap<LicenseMetadata, GitHubLicense>()
        .ForMember(
          d => d.Key,
          s => s.MapFrom(y => y.Key))
        .ForMember(
          d => d.Name,
          s => s.MapFrom(y => y.Name));
    }
  }
}