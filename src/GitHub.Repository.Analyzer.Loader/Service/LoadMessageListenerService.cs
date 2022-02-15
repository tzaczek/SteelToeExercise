using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
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
      Guard.Against.Null(clientData, nameof(clientData));

      _logger.LogInformation($"{nameof(LoadMessageListenerService)} received the message");

      var deserializedClientData = JsonConvert.DeserializeObject<GitHubClientData>(clientData);

      var client = _clientProvider.GetClient(deserializedClientData);
      IList<GitHubRepository> repositories = new List<GitHubRepository>();

      try
      {
        repositories = await client.GetAllStarredRepositoriesForCurrent();
      }
      catch (Exception e)
      {
        _logger.LogError(e, "Error processing GitHub request");

        return JsonConvert.SerializeObject(new LoaderResponse
        {
          Success = false,
          ProcessingMessage = e.ToString()
        });
      }

      return JsonConvert.SerializeObject(new LoaderResponse
      {
        Success = true,
        Results = repositories.Cast<object>().ToList()
      });
    }
  }
}
