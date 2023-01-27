namespace AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
public class TelegramSettings
{
    public string Token { get; set; }
    public string MyChat { get; set; }
    public string EURBRLAlert { get; set; }
}