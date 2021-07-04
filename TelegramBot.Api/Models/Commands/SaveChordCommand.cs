using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class SaveChordCommand : ICommand
    {
        private readonly IBotDbRepository _repository;
        
        public string Name => "/savechord";

        public SaveChordCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            try
            {
                var chord = message.ParseChord();
                await _repository.AddChordAsync(chord);
                await client.SendTextMessageAsync(chatId, $"Chord {chord.Name} saved", replyToMessageId: messageId);
            }
            catch (ArgumentException)
            {
                await client.SendTextMessageAsync(chatId, "Command has wrong number of arguments",
                    replyToMessageId: messageId);
                throw;
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(chatId, $"Exception occured - {ex.GetFullMessage()}",
                    replyToMessageId: messageId);
                throw;
            }
        }
    }
}