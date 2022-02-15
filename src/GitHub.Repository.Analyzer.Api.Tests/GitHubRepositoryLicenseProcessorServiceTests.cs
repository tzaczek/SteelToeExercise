using GitHub.Repository.Analyzer.Tests.Shared.AutoFixture;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using GitHub.Repository.Analyzer.Processor.Client.Client;
using GitHub.Repository.Analyzer.Processor.Client.Models;
using GitHub.Repository_Analyzer.Api.ClientProvider;
using Moq;
using Xunit;
using GitHub.Repository_Analyzer.Api.Service;

namespace GitHub.Repository.Analyzer.Api.Tests
{
  public class GitHubRepositoryLicenseProcessorServiceTests
  {
    [Theory, AutoMoqDefaultData]
    public async Task ProcessRepositoriesWithMatchingLicenseShouldReturnMatchingRepository(
      [Frozen] Mock<ILicenseProcessorClientProvider> licenseProcessorClientProvider,
      Mock<ILicenseProcessorClient> licenseProcessorClient,
      GitHubRepositoryLicenseProcessorService uut)
    {
      //Arrange

      var repositoriesToBeProcessed = new List<GitHubRepository> { new() };

      var processingResult = new ProcessRepositoryLicenseResult
      {
        Result = true
      };
      
      licenseProcessorClient
        .Setup(y => y.Process(It.IsAny<ProcessRepositoryLicenseData>(), It.IsAny<CancellationToken>()))
        .Returns(Task.FromResult(processingResult));

      licenseProcessorClientProvider
        .Setup(y => y.GetClient())
        .Returns(licenseProcessorClient.Object);

      //Act

      var result = await uut.Process(repositoriesToBeProcessed, string.Empty, CancellationToken.None);

      //Assert

      Assert.True(result.Count == 1);
    }
  }
}
