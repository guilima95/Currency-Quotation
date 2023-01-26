using System;
using System.Globalization;
using AzureFunction.CurrentQuotation.Extensions;
using AzureFunction.CurrentQuotation.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunction.CurrentQuotation
{
    public class EuroCurrentQuotation
    {
        private readonly ILogger _logger;
        private readonly IAwesomeAPIRepository _awesomeAPIRepository;
        private readonly ITelegramBotService _telegramBotService;

        public EuroCurrentQuotation(ILoggerFactory loggerFactory, IAwesomeAPIRepository awesomeAPIRepository, ITelegramBotService telegramBotService)
        {
            _logger = loggerFactory.CreateLogger<EuroCurrentQuotation>();
            _awesomeAPIRepository = awesomeAPIRepository;
            _telegramBotService = telegramBotService;
        }

        [Function("EuroCurrentQuotation")]
        public async Task Run([TimerTrigger("* * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _logger.LogInformation("Find quotation currency EUR-BRL...");

            var response = await _awesomeAPIRepository.GetQuotationByCurrencyAsync("EUR-BRL");

            //Send to message for telegram bot if it is the parametered value:
            string? currency = response.Ask?.ToCurrency();

            if (string.IsNullOrEmpty(currency))
            {
                currency = string.Empty;
            }

            string message = $"Currency EUR-BRL: {currency}, Date: {response.CreateDate}";

            await _telegramBotService.SendMessageAsync(message);

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
