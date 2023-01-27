using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.Repositories;
using AzureFunction.CurrentQuotation.Services;
using AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(AzureFunction.CurrentQuotation.TimerTrigger.Startup))]

namespace AzureFunction.CurrentQuotation.TimerTrigger;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<IAwesomeAPIRepository, AwesomeAPIRepository>();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();

        builder.Services.AddOptions<TelegramSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("TelegramSettings").Bind(settings);
            });


        builder.Services.AddLogging();
    }
}
