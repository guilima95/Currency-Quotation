using AzureFunction.CurrentQuotation.Contracts;

namespace AzureFunction.CurrentQuotation.Interfaces;
public interface IAwesomeAPIRepository
{
    Task<TelegramBotResponse> GetQuotationByCurrencyAsync(string currency);
}
