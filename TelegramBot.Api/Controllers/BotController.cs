using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;
using TelegramBot.Api.Services;

namespace TelegramBot.Api.Controllers
{
    [ApiController]
    public class BotController : Controller
    {
        private readonly IBotService _bot;
        private readonly ILogger<BotController> _logger;

        public BotController(IBotService bot, ILogger<BotController> logger)
        {
            _bot = bot;
            _logger = logger;
        }
        
        [HttpPost]
        [Route("api/message/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            try
            {
                await _bot.ExecuteCommand(update.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
                throw;
            }
        }
    }
}