using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Domain;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class GetSongCommand : ICommand
    {
        public string Name => "/getsong";
        private readonly IBotDbRepository _repository;

        public GetSongCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            Song? song = null;
            
            try
            {
                var (author, name) = message.ParseSongAuthorAndName();
                song = await _repository.GetSong(author, name);
            }
            catch (ArgumentException)
            {
                await client.SendTextMessageAsync(chatId, "Command has wrong number of arguments", replyToMessageId: messageId);
            }
            
            if (song == null)
                await client.SendTextMessageAsync(chatId, "Song was not found", replyToMessageId: messageId);
            else
                await client.SendTextMessageAsync(chatId, $"Song {song}", replyToMessageId: messageId);
        }
    }
}