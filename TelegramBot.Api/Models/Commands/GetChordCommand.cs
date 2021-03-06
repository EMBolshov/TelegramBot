using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Domain;
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
        
        public async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            Chord? chord = null;
            
            try
            {
                chord = await _repository.GetChordAsync(message.ParseName());
            }
            catch (ArgumentException)
            {
                await client.SendTextMessageAsync(chatId, "Command has wrong number of arguments", replyToMessageId: messageId);
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(chatId, $"Exception occured - {ex.GetFullMessage()}",
                    replyToMessageId: messageId);
            }
            
            if (chord == null)
                await client.SendTextMessageAsync(chatId, "Chord was not found", replyToMessageId: messageId);
            else
                await client.SendTextMessageAsync(chatId, $"Chord {chord.Name}, Fingering: \n {chord}", replyToMessageId: messageId);
        }
    }
}