using System.Threading.Tasks;
using GitHub.Repository.Analyzer.Processor.Communication;

namespace GitHub.Repository.Analyzer.Processor.Processor
{
  public interface IRepositoryProcessor
  {
    ProcessorType ProcessorType { get; }
    Task<ProcessRepositoryLicenseReply> Process(ProcessRepositoryLicenseRequest request);
  }
}