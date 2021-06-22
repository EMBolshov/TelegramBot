using System;
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
            try
            {
                var cmd = _commands.SingleOrDefault(c => message.Text.StartsWith(c.Name)) ??
                          new SendCommandNotFoundMessageCommand();
                
                return cmd.Execute(message, _client);
            }
            //Suppress exception to prevent retry pending update 
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
                return Task.CompletedTask;
            }
        }
    }
}