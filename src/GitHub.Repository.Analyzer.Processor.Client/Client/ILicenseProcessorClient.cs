using System.Threading;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.Processor.Client.Models;

namespace GitHub.Repository.Analyzer.Processor.Client.Client
{
  public interface ILicenseProcessorClient
  {
    Task<ProcessRepositoryLicenseResult> Process(ProcessRepositoryLicenseData processRepositoryLicenseData, CancellationToken cancellationToken);
  }
}
