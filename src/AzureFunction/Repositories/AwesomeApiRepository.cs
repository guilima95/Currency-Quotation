using AzureFunction.CurrentQuotation.Contracts;
using AzureFunction.CurrentQuotation.Interfaces;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureFunction.CurrentQuotation.Repositories;
public class AwesomeAPIRepository : IAwesomeAPIRepository
{
    public async Task<TelegramBotResponse?> GetQuotationByCurrencyAsync(string currency)
    {
        string url = $"https://economia.awesomeapi.com.br/last/{currency}";

        using HttpClient client = new();
        var result = await client.GetStringAsync(url);

        var response = JsonSerializer.Deserialize<Response>(json: result);

        if (response?.EURBRL is null)
        {
            return new TelegramBotResponse();
        }

        return response.EURBRL;
    }

    public async Task<TelegramBotResponse?> GetQuotationByRangeDateAsync(string currency, DateOnly start, DateOnly end)
    {
        string url = $"https://economia.awesomeapi.com.br/json/daily/{currency}?start_date={start:yyyyMMdd}&end_date={end:yyyyMMdd}";

        using HttpClient client = new();
        var result = await client.GetStringAsync(url);

        var response = JsonSerializer.Deserialize<List<EURBRL>>(json: result);

        if (response is null)
        {
            return new EURBRL();
        }

        return response[0];
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

        public static implicit operator TelegramBotResponse(EURBRL eurBrl)
        {
            return new TelegramBotResponse(
                eurBrl.Code, eurBrl.High, eurBrl.Low, eurBrl.VarBid, eurBrl.Ask, eurBrl.CreateDate);
        }
    }
}
