using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Api.Models;
using TelegramBot.Api.Models.Commands;

namespace TelegramBot.Api.Services
{
    public class BotService : IBotService
    {
        private readonly TelegramBotClient _client;
        private readonly IReadOnlyCollection<ICommand> _commands;
        private readonly ILogger<BotService> _logger;

        public BotService(BotSettings settings, ILogger<BotService> logger)
        {
            _logger = logger;
            _client = settings.Client;
            _commands = settings.Commands;
        }

        public Task ExecuteCommand(Message message)
        {
            _logger.LogWarning($"Message.Text: > {message.Text}");
            _commands.ToList().ForEach(c => _logger.LogWarning($"Command: {c.Name}"));
            var cmd = _commands.Single(c => message.Text.StartsWith(c.Name));
            _logger.LogWarning($"Message.Text: {message.Text}, Command: {cmd.Name}");
            return cmd.Execute(message, _client);
        }
    }
}