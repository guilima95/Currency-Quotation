using AzureFunction.CurrentQuotation.Contracts;

namespace AzureFunction.CurrentQuotation.Interfaces;
public interface IAwesomeAPIRepository
{
    Task<TelegramBotResponse?> GetQuotationByCurrencyAsync(string currency);

    Task<TelegramBotResponse?> GetQuotationByRangeDateAsync(string currency, DateOnly start, DateOnly end);
}
