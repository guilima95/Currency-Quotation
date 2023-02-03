using AzureFunction.CurrentQuotation.Contracts;
using AzureFunction.CurrentQuotation.Extensions;
using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.BotAPI.AvailableTypes;

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

            var responseCurrent = await _awesomeAPIRepository.GetQuotationByCurrencyAsync("EUR-BRL");

            DateTime dateCurrent = DateTime.Parse(responseCurrent?.CreateDate ?? string.Empty);
            var start = DateOnly.FromDateTime(dateCurrent.ToUniversalTime().AddDays(-2));
            var finish = DateOnly.FromDateTime(dateCurrent.ToUniversalTime().AddDays(-1));

            var responseMinutesAgo = await _awesomeAPIRepository
               .GetQuotationByRangeDateAsync("EUR-BRL", start, finish);

            string? currency = responseCurrent?.Ask?.ToCurrency();
            if (string.IsNullOrEmpty(currency))
            {
                currency = string.Empty;
            }

            string? currencyBefore = responseMinutesAgo?.Ask?.ToCurrency();
            if (string.IsNullOrEmpty(currencyBefore))
            {
                currencyBefore = string.Empty;
            }

            string message = "Currency EUR-BRL";

            if (currency.ToDecimal() < currencyBefore.ToDecimal())
            {
                message = $"{message} is Down: {currency}, Date: {responseCurrent?.CreateDate}";             
            }
            else
            {
                message = $"{message} has increased or is stable compared to the last day: {currency}, Date: {responseCurrent?.CreateDate}";
            }

            await _telegramBotService.ProcessBotAsync(message);

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
