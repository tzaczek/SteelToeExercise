namespace GitHub.Repository_Analyzer.Api.Cache
{
  public static class CacheKeyBuilder
  {
    public static string Build(string accessToken, string licenseName)
    {
      return $"{accessToken}-{licenseName}";
    }
  }
}
