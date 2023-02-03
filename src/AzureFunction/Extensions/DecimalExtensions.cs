using System.Globalization;
using System.Text.RegularExpressions;

namespace AzureFunction.CurrentQuotation.Extensions;
public static class StringExtensions
{
    public static string ToCurrency(this string value)
    {
        var convertDecimal = Decimal.Parse(value,
        NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

        var currencyFormatted = Math.Round(convertDecimal, 2);

        return currencyFormatted.ToString(CultureInfo.InvariantCulture);
    }
}

public static class DecimalExtensions
{
    public static decimal ToDecimal(this string value)
    {
        var convertDecimal = Decimal.Parse(value,
        NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);

        var currencyFormatted = Math.Round(convertDecimal, 2);

        return currencyFormatted;
    }
}