using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class GetChordCommand : ICommand
    {
        private readonly IBotDbRepository _repository;
        public string Name => "/getchord";

        public GetChordCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chord = await _repository.GetChord(message.ParseName());
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            if (chord == null)
                await client.SendTextMessageAsync(chatId, "Chord not found", replyToMessageId: messageId);
            else
                await client.SendTextMessageAsync(chatId, $"Chord {chord.Name}, Fingering: \n {chord}", replyToMessageId: messageId);
        }
    }
}