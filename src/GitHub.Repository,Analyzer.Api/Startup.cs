using System;
using System.Text.Json.Serialization;
using GitHub.Repository.Analyzer.Loader.Communication;
using GitHub.Repository.Analyzer.Processor.Client.ClientBuilder;
using GitHub.Repository_Analyzer.Api.Cache;
using GitHub.Repository_Analyzer.Api.ClientProvider;
using GitHub.Repository_Analyzer.Api.Controllers;
using GitHub.Repository_Analyzer.Api.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using Steeltoe.Connector.Redis;

namespace GitHub.Repository_Analyzer.Api
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
      services
        .AddControllers()
        .AddJsonOptions(options =>
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

      services.AddOpenApiDocument(settings =>
      {
        settings.DocumentName = "Repository Analyzer";
        settings.Version = "v0.0.1";
        settings.Title = "GitHub Repository Analyzer";
        settings.Description = "API for GitHub repositories processing";
      });

      services.AddRabbitQueue(new Queue(MessagingQueueNames.LoadRepositoryQueueName));
      services.AddRabbitTemplate((p, t) =>
      {
        t.ReplyTimeout = 60000;
        t.UseDirectReplyToContainer = true;
        t.ServiceName = "fixedReplyQRabbitTemplate";
      });

      services.AddGrpc();
      services.AddAutoMapper(typeof(ILicenseProcessorClientBuilder));
      services.AddSingleton<ILicenseProcessorClientBuilder, GrpcLicenseProcessorClientBuilder>();

      services.AddSingleton<ILicenseProcessorClientProvider, LicenseProcessorClientProvider>();
      services.AddSingleton<ICacheStorage, RedisCacheStorage>();
      services.AddSingleton<GitHubRepositoryLoaderService>();
      services.AddSingleton<GitHubRepositoryLicenseProcessorService>();

      services.AddDistributedRedisCache(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseOpenApi();
      app.UseSwaggerUi3();

      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
