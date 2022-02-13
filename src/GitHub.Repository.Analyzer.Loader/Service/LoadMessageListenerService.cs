using System.Threading.Tasks;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.Loader.ClientProvider;
using GitHub.Repository.Analyzer.Loader.Communication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Messaging.RabbitMQ.Attributes;

namespace GitHub.Repository.Analyzer.Loader.Service
{
  public class LoadMessageListenerService
  {
    private readonly ILogger<LoadMessageListenerService> _logger;
    private readonly IGitHubClientProvider _clientProvider;

    public LoadMessageListenerService(ILogger<LoadMessageListenerService> logger, IGitHubClientProvider clientProvider)
    {
      _logger = logger;
      _clientProvider = clientProvider;
    }

    [RabbitListener(MessagingQueueNames.LoadRepositoryQueueName)]
    public async Task<string> Execute(string clientData)
    {
      _logger.LogInformation($"{nameof(LoadMessageListenerService)} received the message");

      var deserializedClientData = JsonConvert.DeserializeObject<GitHubClientData>(clientData);

      var client = _clientProvider.GetClient(deserializedClientData);
      var repositories = await client.GetAllStarredRepositoriesForCurrent();

      return JsonConvert.SerializeObject(repositories);
    }
  }
}
