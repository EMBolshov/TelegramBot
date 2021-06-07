using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Api.Models.Commands
{
    public class EchoCommand : ICommand
    {
        public string Name => "echo";
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, $"Echo: {message.Text}", replyToMessageId: messageId);
        }
    }
}