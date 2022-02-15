using AutoFixture.Xunit2;
using GitHub.Repository.Analyzer.GitHub.Client.Client;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.Loader.ClientProvider;
using GitHub.Repository.Analyzer.Tests.Shared.AutoFixture;
using Moq;
using Xunit;

namespace GitHub.Repository.Analyzer.Loader.Tests
{
  public class CachedGitHubClientProviderTests
  {
    [Theory, AutoMoqDefaultData]
    public void GetClientWhichDoesNotExistInCacheShouldCreateNewClient(
      [Frozen] Mock<IGitHubClientProvider> targetClientProvider,
      Mock<IGitHubClient> targetResult,
      CachedGitHubClientProvider sut)
    {
      //Arrange

      var resultClient = targetResult.Object;

      targetClientProvider
        .Setup(y => y.GetClient(It.IsAny<GitHubClientData>()))
        .Returns(resultClient);

      //Act

      var result = sut.GetClient(new GitHubClientData());

      //Assert

      Assert.Equal(resultClient, result);
    }

    [Theory, AutoMoqDefaultData]
    public void GetClientWhichExistInCacheShouldCallTargetOnlyOnce(
      [Frozen] Mock<IGitHubClientProvider> targetClientProvider,
      Mock<IGitHubClient> targetResult,
      CachedGitHubClientProvider sut)
    {
      //Arrange

      var gitHubClientData = new GitHubClientData()
      {
        AccessToken = "token",
        OrganizationName = "SteelToe"
      };

      var resultClient = targetResult.Object;

      targetClientProvider
        .Setup(y => y.GetClient(It.IsAny<GitHubClientData>()))
        .Returns(resultClient);

      //Act

      sut.GetClient(gitHubClientData);
      sut.GetClient(gitHubClientData);

      //Assert

      targetClientProvider.Verify(y => y.GetClient(gitHubClientData), Times.Once);
    }
  }
}
