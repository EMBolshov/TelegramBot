using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
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

        public async Task ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            var chordNames = message.Text.Split(new [] {' ', ',', '|'}, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToHashSet();
            
            var chords = (await _repository.GetChordsAsync(chordNames)).ToList();

            if (!chords.Any())
            {
                await client.SendTextMessageAsync(chatId, $"Chords {string.Join(", ", chordNames)} was not found", replyToMessageId: messageId);
            }
            else
            {
                foreach (var chord in chords)
                {
                    await client.SendTextMessageAsync(chatId, $"{chord}", replyToMessageId: messageId);
                }
            }
        }
    }
}