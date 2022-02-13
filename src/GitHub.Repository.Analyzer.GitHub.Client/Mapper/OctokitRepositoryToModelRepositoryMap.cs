using AutoMapper;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using Octokit;

namespace GitHub.Repository.Analyzer.GitHub.Client.Mapper
{
  public class OctokitRepositoryToModelRepositoryMap : Profile
  {
    public OctokitRepositoryToModelRepositoryMap()
    {
      CreateMap<Octokit.Repository, GitHubRepository>()
        .ForMember(
          d => d.Name,
          s => s.MapFrom(y => y.FullName))
        .ForMember(
          d => d.Url,
          s => s.MapFrom(y => y.Url))
        .ForMember(
          d => d.Id,
          s => s.MapFrom(y => y.Id))
      .ForMember(
      d => d.License,
      s => s.MapFrom(y => y.License));
    }
  }
}