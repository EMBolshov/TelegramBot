using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Api.Models.Commands
{
    public interface ICommand
    {
        public string Name { get; }
        Task Execute(Message message, TelegramBotClient client);
    }
}