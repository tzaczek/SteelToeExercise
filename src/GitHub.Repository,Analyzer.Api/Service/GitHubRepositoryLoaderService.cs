using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.GitHub.Client.Models;
using GitHub.Repository.Analyzer.Loader.Communication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Steeltoe.Messaging;
using Steeltoe.Messaging.RabbitMQ.Core;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using Steeltoe.Messaging.RabbitMQ.Support;

namespace GitHub.Repository_Analyzer.Api.Service
{
  public class GitHubRepositoryLoaderService
  {
    private readonly ILogger<GitHubRepositoryLoaderService> _logger;
    private readonly RabbitTemplate _rabbitTemplate;

    public GitHubRepositoryLoaderService(ILogger<GitHubRepositoryLoaderService> logger, RabbitTemplate rabbitTemplate)
    {
      _logger = logger;
      _rabbitTemplate = rabbitTemplate;
    }

    public async Task<IList<GitHubRepository>> LoadStarredRepositories(GitHubClientData clientData)
    {
      _logger.LogDebug("Load starred repositories");

      var serializedClientData = JsonConvert.SerializeObject(clientData);
      var message = CreateMessage(serializedClientData);

      var result = await _rabbitTemplate.ConvertSendAndReceiveAsync<string>(
        new RabbitDestination(MessagingQueueNames.LoadRepositoryQueueName),
        message);

      var deserializedResult =  JsonConvert.DeserializeObject<IList<GitHubRepository>>(result);

      _logger.LogDebug($"Received {deserializedResult.Count} repositories count");

      return deserializedResult;
    }

    private static IMessage CreateMessage(string messagePayload)
    {
      return RabbitMessageBuilder
        .WithPayload(Encoding.UTF8.GetBytes(messagePayload))
        .SetContentType(MessageHeaders.CONTENT_TYPE_TEXT_PLAIN)
        .SetMessageId(Guid.NewGuid().ToString())
        .Build();
    }
  }
}
