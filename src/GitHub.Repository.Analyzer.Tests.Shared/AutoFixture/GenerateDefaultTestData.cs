using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace GitHub.Repository.Analyzer.Tests.Shared.AutoFixture
{
  public class AutoMoqDefaultDataAttribute : AutoDataAttribute
  {
    public AutoMoqDefaultDataAttribute() : base(GetDefaultFixture)
    {
    }

    public static IFixture GetDefaultFixture()
    {
      var autoMoqCustomization = new AutoMoqCustomization();

      return new Fixture()
        .Customize(autoMoqCustomization);
    }
  }
}
