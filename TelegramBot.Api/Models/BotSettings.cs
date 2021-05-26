using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using TelegramBot.Api.Models.Commands;

namespace TelegramBot.Api.Models
{
    public class BotSettings
    {
        public readonly TelegramBotClient Client;
        public readonly IReadOnlyCollection<ICommand> Commands;

        public BotSettings(IOptions<Options> options)
        {
            Commands = new List<ICommand>
            {
                new TestCommand()
            };

            Client = new TelegramBotClient(options.Value.AccessToken);

            var hook = $"{options.Value.AppBaseUrl}/api/message/update";
            
            Client.SetWebhookAsync(hook);
        }
        
        public class Options
        {
            public const string Name = nameof(BotSettings);
            
            public string AppBaseUrl { get; set; }
            public string AccessToken { get; set; }
        }
    }
}