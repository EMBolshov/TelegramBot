using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Domain;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class GetChordsCommand : ICommand
    {
        public string Name => "/getchords";
        private readonly IBotDbRepository _repository;

        public GetChordsCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var chordNames = message.Text.Split(' ').Skip(1);
            
            HashSet<Chord> chords = (await _repository.GetChords(chordNames)).ToHashSet();

            if (chords.Any())
                await client.SendTextMessageAsync(chatId, "Chords was not found", replyToMessageId: messageId);
            else
            {
                foreach (var chord in chords)
                {
                    await client.SendTextMessageAsync(chatId, $"Chord {chord.Name}, Fingering: \n {chords}",
                        replyToMessageId: messageId);
                }
            }
        }
    }
}