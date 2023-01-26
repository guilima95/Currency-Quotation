using Telegram.BotAPI.AvailableTypes;

namespace AzureFunction.CurrentQuotation.Interfaces;
public interface ITelegramBotService
{
    Task<User> GetUserTelegramAsync();

    Task SendMessageAsync(string message);
}
