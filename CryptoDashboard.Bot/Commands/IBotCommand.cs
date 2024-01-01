using Telegram.Bot.Types;

namespace CryptoDashboard.Bot.Commands
{
    public interface IBotCommand
    {
        string Command { get; }
        string Description { get; }
        Task<Message> Execute(Message message);
    }
}
