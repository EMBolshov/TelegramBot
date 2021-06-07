using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BotService(BotSettings settings)
        {
            _client = settings.Client;
            _commands = settings.Commands;
        }

        public Task ExecuteCommand(Message message)
        {
            return _commands.Single(c => message.Text.Contains(c.Name)).Execute(message, _client);
        }
    }
}