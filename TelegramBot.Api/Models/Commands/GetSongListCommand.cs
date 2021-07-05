using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Extensions;
using TelegramBot.Domain;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models.Commands
{
    public class GetSongListCommand : ICommand
    {
        public string Name => "/getsongs";
        private readonly IBotDbRepository _repository;

        public GetSongListCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }
        
        public async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var songs = new List<Song>();
            
            try
            {
                songs = (await _repository.GetSongListAsync()).ToList();
            }
            catch (Exception ex)
            {
                await client.SendTextMessageAsync(chatId, $"Exception occured - {ex.GetFullMessage()}",
                    replyToMessageId: messageId);
            }
            
            if (!songs.Any())
                await client.SendTextMessageAsync(chatId, "Songs was not found", replyToMessageId: messageId);
            else
            {
                await client.SendTextMessageAsync(chatId, "All available songs:", replyToMessageId: messageId);
                foreach (var song in songs)
                {
                    await client.SendTextMessageAsync(chatId, $"{song.Author} -- {song.Name}");
                }
            }
        }
    }
}