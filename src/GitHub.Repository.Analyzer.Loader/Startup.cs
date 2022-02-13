using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GitHub.Repository.Analyzer.GitHub.Client.ClientBuilder;
using GitHub.Repository.Analyzer.Loader.ClientProvider;
using GitHub.Repository.Analyzer.Loader.Communication;
using GitHub.Repository.Analyzer.Loader.Service;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;

namespace GitHub.Repository.Analyzer.Loader
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRabbitQueue(new Queue(MessagingQueueNames.LoadRepositoryQueueName));

      services.AddSingleton<LoadMessageListenerService>();
      services.AddRabbitListeners<LoadMessageListenerService>();

      services.AddSingleton<IGitHubClientBuilder, DefaultGitHubClientBuilder>();

      services.AddSingleton<IGitHubClientProvider, GitHubClientProvider>();
      services.Decorate<IGitHubClientProvider, CachedGitHubClientProvider>();

      services.AddAutoMapper(typeof(IGitHubClientBuilder));
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
  }
}
