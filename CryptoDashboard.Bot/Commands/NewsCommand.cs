using CryptoDashboard.Bot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CryptoDashboard.Bot.Commands
{
    public class NewsCommand : IBotCommand
    {
        private readonly NewsService _service;
        private readonly ITelegramBotClient _bot;

        public NewsCommand(NewsService service, ITelegramBotClient bot)
        {
            _service = service;
            _bot = bot;
        }
        public string Command => "/news";

        public string Description => "Command for getting news from headlines";

        public async Task<Message> Execute(Message message)
        {
            var newsList = await _service.GetTopHeadlines();
            var buttons = new InlineKeyboardMarkup(newsList.Articles.Select(x =>
            {
                return new List<InlineKeyboardButton>{
                    InlineKeyboardButton.WithUrl(x.Title.Substring(0, 40), x.Url)
                };
            }).Take(10));
            return await _bot.SendTextMessageAsync(message.Chat.Id, "Top news:", replyMarkup: buttons);
        }
    }
}
