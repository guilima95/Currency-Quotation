using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;

namespace AzureFunction.CurrentQuotation;
public class TelegramBot
{
    private const string BotToken = "23206361:edb6cf78579c2df6981f7555a3286ba9";

    public async Task<User> GetUserTelegramAsync()
    {
        var api = new BotClient(BotToken);
        return await api.GetMeAsync();
    }
}
