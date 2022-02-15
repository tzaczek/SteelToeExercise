using System;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.Processor.Communication;
using GitHub.Repository.Analyzer.Processor.Processor;
using GitHub.Repository.Analyzer.Tests.Shared.AutoFixture;
using Xunit;

namespace GitHub.Repository.Analyzer.Processor.Tests
{
  public class LicenseKeyNameProcessorTests
  {
    [Theory, AutoMoqDefaultData]
    public async Task ProcessNullRequestShouldThrowException(LicenseKeyNameProcessor sut)
    {
      //Arrange

      ProcessRepositoryLicenseRequest request = null;

      //Act
      //Assert

      await Assert.ThrowsAnyAsync<Exception>(() => sut.Process(request));
    }

    [Theory, AutoMoqDefaultData]
    public async Task ProcessRequestWithEmptyLicenseShouldReturnFalse(LicenseKeyNameProcessor sut)
    {
      //Arrange

      var request = new ProcessRepositoryLicenseRequest
      {
        LicenseKey = string.Empty,
        LicenseKeySearchDefinition = string.Empty,
        LicenseName = string.Empty,
        RepositoryName = string.Empty
      };

      //Act

      var result = await sut.Process(request);

      //Assert

      Assert.False(result.ProcessingResult);
    }


    [Theory, AutoMoqDefaultData]
    public async Task ProcessRequestWithMatchingLicenseShouldReturnTrue(LicenseKeyNameProcessor sut)
    {
      //Arrange

      var request = new ProcessRepositoryLicenseRequest
      {
        LicenseKey = "apache-123",
        LicenseKeySearchDefinition = "apache",
        LicenseName = "apache 2.0",
        RepositoryName = "SteelToe"
      };

      //Act

      var result = await sut.Process(request);

      //Assert

      Assert.True(result.ProcessingResult);
    }

    [Theory, AutoMoqDefaultData]
    public async Task ProcessRequestWithMatchingAndDifferentCasingLicenseShouldReturnTrue(LicenseKeyNameProcessor sut)
    {
      //Arrange

      var request = new ProcessRepositoryLicenseRequest
      {
        LicenseKey = "apache-123",
        LicenseKeySearchDefinition = "aPaChE",
        LicenseName = "apache 2.0",
        RepositoryName = "SteelToe"
      };

      //Act

      var result = await sut.Process(request);

      //Assert

      Assert.True(result.ProcessingResult);
    }
  }
}