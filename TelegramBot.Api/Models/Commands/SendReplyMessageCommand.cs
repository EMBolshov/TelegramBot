using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Api.Models.Commands
{
    public class SendReplyMessageCommand : ICommand
    {
        public string Name { get; }

        private readonly string _message;

        public SendReplyMessageCommand(string message)
        {
            _message = message;
        }
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId, _message, replyToMessageId: messageId);
        }
    }
}