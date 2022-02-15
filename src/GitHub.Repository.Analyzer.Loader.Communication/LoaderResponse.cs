using System.Collections.Generic;

namespace GitHub.Repository.Analyzer.Loader.Communication
{
  public class LoaderResponse
  {
    public bool Success { get; set; }
    public string ProcessingMessage { get; set; }
    public IList<object> Results { get; set; }
  }
}
