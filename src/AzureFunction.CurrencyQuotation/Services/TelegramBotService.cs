using AzureFunction.CurrentQuotation.Interfaces;
using AzureFunction.CurrentQuotation.TimerTrigger.Contracts;
using Microsoft.Extensions.Options;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace AzureFunction.CurrentQuotation.Services;
public class TelegramBotService : ITelegramBotService
{
    private readonly IOptions<TelegramSettings> _settings;

    public TelegramBotService(IOptions<TelegramSettings> settings)
    {
        _settings = settings;
    }

    public async Task ProcessBotAsync(string message)
    {
        BotClient _botClient = new(_settings.Value.Token);
        await _botClient.SendMessageAsync(_settings.Value.MyChat, $"Attention!!! Euro down: {message}");
    }
}
