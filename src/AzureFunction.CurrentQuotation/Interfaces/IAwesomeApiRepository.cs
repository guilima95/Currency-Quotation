using AzureFunction.CurrentQuotation.Contracts;

namespace AzureFunction.CurrentQuotation.Interfaces;
public interface IAwesomeAPIRepository
{
    Task<CurrencyResponse> GetQuotationByCurrencyAsync(string currency);
}
