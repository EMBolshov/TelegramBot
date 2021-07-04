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
    public class GetChordsForSongCommand : ICommand
    {
        public string Name => "/getchordsforsong";
        private readonly IBotDbRepository _repository;

        public GetChordsForSongCommand(IBotDbRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var chords = new HashSet<Chord>();
            
            try
            {
                var (author, name) = message.ParseSongAuthorAndName();
                chords = (await _repository.GetChordsForSong(author, name)).ToHashSet();
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
            
            if (chords.Any())
                await client.SendTextMessageAsync(chatId, "Chords was not found for requested song", replyToMessageId: messageId);
            else
                await client.SendTextMessageAsync(chatId, $"Chords: {string.Join(", ", chords)}", replyToMessageId: messageId);
        }
    }
}