using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Api.Services
{
    public interface IBotService
    {
        Task ExecuteCommand(Message message);
    }
}