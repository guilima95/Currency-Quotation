using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.Repositories;
using AzureFunction.CurrentQuotation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IAwesomeAPIRepository, AwesomeAPIRepository>();
        
        services.AddHttpClient();

        services.AddSingleton<ITelegramBotService, TelegramBotService>();
    })
    .Build();

host.Run();
