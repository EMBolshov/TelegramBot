using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramBot.Api.Models.Commands;
using TelegramBot.Infrastructure;

namespace TelegramBot.Api.Models
{
    public class BotSettings
    {
        public readonly TelegramBotClient Client;
        public readonly IReadOnlyCollection<ICommand> Commands;
        private readonly ILogger<BotSettings> _logger;
        
        public BotSettings(IOptions<Options> options, IBotDbRepository repository, ILogger<BotSettings> logger)
        {
            _logger = logger;
            Commands = new List<ICommand>
            {
                new EchoCommand(),
                new SaveChordCommand(repository),
                new SaveSongCommand(repository)
            };

            Client = new TelegramBotClient(options.Value.AccessToken);

            var hook = $"{options.Value.AppBaseUrl}/api/message/update";
            
            Client.SetWebhookAsync(hook);
            _logger.LogWarning($"Webhook: {hook}");
        }

        public class Options
        {
            public const string Name = nameof(BotSettings);
            
            public string AppBaseUrl { get; set; }
            public string AccessToken { get; set; }
        }
    }
}