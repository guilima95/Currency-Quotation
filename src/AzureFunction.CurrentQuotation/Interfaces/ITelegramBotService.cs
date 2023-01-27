namespace AzureFunction.CurrentQuotation.Interfaces;
public interface ITelegramBotService
{
    Task ProcessBotAsync(string message);
}
