using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.Repositories;
using AzureFunction.CurrentQuotation.Services;
using AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


public class Program
{

    public static async Task Main(string[] args)
    {
        var builder = Host
          .CreateDefaultBuilder(args)
          .ConfigureFunctionsWorkerDefaults()
          .ConfigureAppConfiguration((hostingContext, configBuilder) =>
          {
              var env = hostingContext.HostingEnvironment;
              configBuilder
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"local.settings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
          })
          .ConfigureServices((appBuilder, services) =>
          {
              var configuration = appBuilder.Configuration;
              services.AddScoped<IAwesomeAPIRepository, AwesomeAPIRepository>();
              services.AddHttpClient();
              services.AddSingleton<ITelegramBotService, TelegramBotService>();

              services.AddOptions<TelegramSettings>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection("TelegramSettings").Bind(settings);
               });
          });

        await builder.Build().RunAsync();
    }
}
