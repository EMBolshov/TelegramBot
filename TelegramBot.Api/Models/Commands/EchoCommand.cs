using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Api.Models.Commands
{
    public class EchoCommand : ICommand
    {
        private readonly ILogger<EchoCommand> _logger;

        public EchoCommand()
        {
            
        }
        
        public EchoCommand(ILogger<EchoCommand> logger)
        {
            _logger = logger;
        }

        public string Name => "/echo";
        
        public async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            _logger.LogWarning($"ChatId: {chatId}, MessageId: {messageId}");
            try
            {
                await client.SendTextMessageAsync(chatId, $"Echo: {message.Text}", replyToMessageId: messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}