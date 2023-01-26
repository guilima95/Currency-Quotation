namespace AzureFunction.CurrentQuotation.Contracts;
public record CurrencyResponse(
    string? Code, string? High,
    string? Low, string? VarBid, string? Ask, string? CreateDate)
{
    public CurrencyResponse() : this("", "", "", "", "", "") { }
}
