using AzureFunction.CurrentQuotation.Extensions;
using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureFunction.CurrentQuotation
{
    public class UpdateEuroCurrencyQuotation
    {
        private readonly ILogger _logger;
        private readonly IAwesomeAPIRepository _awesomeAPIRepository;
        private readonly ITelegramBotService _telegramBotService;
        private readonly IOptions<TelegramSettings> _settings;

        public UpdateEuroCurrencyQuotation(
            ILoggerFactory loggerFactory,
            IOptions<TelegramSettings> settings,
            IAwesomeAPIRepository awesomeAPIRepository,
            ITelegramBotService telegramBotService)
        {
            _logger = loggerFactory.CreateLogger<UpdateEuroCurrencyQuotation>();
            _settings = settings;
            _awesomeAPIRepository = awesomeAPIRepository;
            _telegramBotService = telegramBotService;
        }

        [Function("UpdateEuroCurrencyQuotation")]
        public async Task Run([TimerTrigger("* * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _logger.LogInformation("Find quotation currency EUR-BRL...");

            var response = await _awesomeAPIRepository.GetQuotationByCurrencyAsync("EUR-BRL");

            string? currency = response.Ask?.ToCurrency();

            if (string.IsNullOrEmpty(currency))
            {
                currency = string.Empty;
            }


            string message = $"Currency EUR-BRL: {currency}, Date: {response.CreateDate}";

            if (currency == _settings.Value.EURBRLAlert)
            {
                await _telegramBotService.ProcessBotAsync(currency);
            }

            _logger.LogInformation(message);

            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
