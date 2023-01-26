using AzureFunction.CurrentQuotation.Interfaces;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;

namespace AzureFunction.CurrentQuotation.Services;
public class TelegramBotService : ITelegramBotService
{
    private const string BotToken = "5905821203:AAHXgakC03_n4WRy-ho5uQwahf-JHjCW3Dc";
    private const int MyChat = 595962405;

    public async Task<User> GetUserTelegramAsync()
    {
        var api = new BotClient(BotToken);
        return await api.GetMeAsync();
    }

    public async Task SendMessageAsync(string message)
    {
        var api = new BotClient(BotToken);

        api.SendMessage(new SendMessageArgs(MyChat, message));
    }
}
