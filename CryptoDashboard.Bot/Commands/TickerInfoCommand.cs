using CryptoDashboard.Bot.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CryptoDashboard.Bot.Commands
{
    public class TickerInfoCommand : IBotCommand
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITelegramBotClient _bot;
        private readonly TickerInfoHelper _tickerInfoHelper;

        public TickerInfoCommand(IServiceScopeFactory serviceScopeFactory) 
        {
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = serviceScopeFactory.CreateScope();
            _tickerInfoHelper = scope.ServiceProvider.GetService<TickerInfoHelper>();
            _bot = scope.ServiceProvider.GetService<ITelegramBotClient>();
        }
        public string Command => "/ticker";

        public string Description => "Show ticker info";

        public async Task<Message> Execute(Message message)
        {
            var ticker = message.Text.Replace(Command, string.Empty).ToUpper();

            var info = await _tickerInfoHelper.GetTickerInfo(ticker);

            return await _bot.SendTextMessageAsync(message.Chat.Id, info.ToString(), parseMode: ParseMode.Html);
        }
    }
}
