using AzureFunction.CurrentQuotation.Contracts;
using AzureFunction.CurrentQuotation.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AzureFunction.CurrentQuotation.Repositories;
public class AwesomeAPIRepository : IAwesomeAPIRepository
{
    public async Task<CurrencyResponse> GetQuotationByCurrencyAsync(string currency)
    {
        string url = $"https://economia.awesomeapi.com.br/last/{currency}";

        using HttpClient client = new();
        var result = await client.GetStringAsync(url);

        var response = JsonSerializer.Deserialize<Response>(json: result);

        if (response?.EURBRL is null)
        {
            return new CurrencyResponse();
        }

        return response.EURBRL;
    }

    public class Response
    {
        public EURBRL? EURBRL { get; set; }
    }

    public class EURBRL
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("high")]
        public string? High { get; set; }

        [JsonPropertyName("low")]
        public string? Low { get; set; }

        [JsonPropertyName("varBid")]
        public string? VarBid { get; set; }

        [JsonPropertyName("ask")]
        public string? Ask { get; set; }

        [JsonPropertyName("create_date")]
        public string? CreateDate { get; set; }

        public static implicit operator CurrencyResponse(EURBRL eurBrl)
        {
            return new CurrencyResponse(
                eurBrl.Code, eurBrl.High, eurBrl.Low, eurBrl.VarBid, eurBrl.Ask, eurBrl.CreateDate);
        }
    }
}
