using System.Collections.Generic;
using System.Linq;

namespace GitHub.Repository_Analyzer.Api.Cache
{
  public static class CacheKeyBuilder
  {
    public static string Build(List<object> values)
    {
      return string.Join("-", values.Select(y => y.ToString()));
    }
  }
}
