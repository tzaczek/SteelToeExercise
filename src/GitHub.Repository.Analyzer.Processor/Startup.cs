using GitHub.Repository.Analyzer.Processor.Processor;
using GitHub.Repository.Analyzer.Processor.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GitHub.Repository.Analyzer.Processor
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddGrpc();
      services.AddSingleton<IRepositoryProcessor, LicenseKeyNameProcessor>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGrpcService<ProcessRepositoryLicenseService>();

        endpoints.MapGet("/", async context =>
              {
            await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
          });
      });
    }
  }
}
