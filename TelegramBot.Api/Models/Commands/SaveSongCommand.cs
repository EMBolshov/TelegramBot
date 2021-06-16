using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class SaveSongCommand : ICommand
    {
        private readonly IBotDbRepository _repository;
        
        public string Name => "/savesong";

        public SaveSongCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Message message, TelegramBotClient client)
        {
            _repository.AddSong(message.ParseSong());
        }
    }
}