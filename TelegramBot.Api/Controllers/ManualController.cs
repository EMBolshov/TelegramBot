using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramBot.Api.Services;

namespace TelegramBot.Api.Controllers
{
    [ApiController]
    [Route("api/manual")]
    public class ManualController : Controller
    {
        private readonly IBotService _bot;
        private readonly ILogger<ManualController> _logger;
        
        public ManualController(IBotService bot, ILogger<ManualController> logger)
        {
            _bot = bot;
            _logger = logger;
        }

        [HttpPost]
        [Route("test")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            var msg = new Message
            {
                Text = "savechord Am e:O||---|---|---|h:-||-O-|---|---|g:-||---|-O-|---|d:-||---|-O-|---|A:O||---|---|---|E:O||---|---|---|"
            };

            await _bot.ExecuteCommand(msg);

            return Ok();
        }

        [HttpGet]
        [Route("healthcheck")]
        public OkResult HealthCheck()
        {
            var message = "I'm alive";
            _logger.LogWarning($"{message}");
            return Ok();
        }
    }
}