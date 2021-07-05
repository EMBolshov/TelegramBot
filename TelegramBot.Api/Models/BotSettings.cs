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

        public BotSettings(IOptions<Options> options, IBotDbRepository repository, ILogger<BotSettings> logger)
        {
            Commands = new List<ICommand>
            {
                new EchoCommand(),
                new SaveChordCommand(repository),
                new GetChordCommand(repository),
                new GetChordsCommand(repository),
                new SaveSongCommand(repository),
                new GetSongCommand(repository),
                new GetChordsForSongCommand(repository),
                new GetSongListCommand(repository)
            };

            Client = new TelegramBotClient(options.Value.AccessToken);

            var hook = $"{options.Value.AppBaseUrl}/api/message/update";

            Client.DeleteWebhookAsync();
            Client.SetWebhookAsync(hook);
            logger.LogWarning($"Webhook: {hook}");
        }

        public TelegramBotClient InitBot()
        {
            return Client;
        }

        public class Options
        {
            public const string Name = nameof(BotSettings);
            
            public string AppBaseUrl { get; set; }
            public string AccessToken { get; set; }
        }
    }
}