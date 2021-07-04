using System;
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
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            
            try
            {
                var song = message.ParseSong();
                await _repository.AddSong(song);
                await client.SendTextMessageAsync(chatId, $"Song {song.Name} by {song.Author} saved", 
                    replyToMessageId: messageId);
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