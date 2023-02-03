namespace AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
public class TelegramSettings
{
    public string Token { get; set; } = null!;
    public string MyChat { get; set; } = null!;
}