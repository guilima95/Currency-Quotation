namespace AzureFunction.CurrentQuotation.Contracts;
public record TelegramBotResponse(
    string? Code, string? High,
    string? Low, string? VarBid, string? Ask, string? CreateDate)
{
    public TelegramBotResponse() : this("", "", "", "", "", "") { }
}
