using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace GitHub.Repository_Analyzer.Api.Cache
{
  public static class CacheKeyBuilder
  {
    public static string Build(List<object> values)
    {
      Guard.Against.Null(values, nameof(values));

      return string.Join("-", values.Select(y => y.ToString()));
    }
  }
}
