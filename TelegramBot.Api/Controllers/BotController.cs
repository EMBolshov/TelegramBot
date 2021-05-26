using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Api.Services;

namespace TelegramBot.Api.Controllers
{
    [ApiController]
    public class BotController : Controller
    {
        private readonly IBotService _bot;

        public BotController(IBotService bot)
        {
            _bot = bot;
        }
        
        [HttpGet]
        [Route("api/message/update")]
        public async Task<OkResult> Test([FromBody] Update update)
        {
            await _bot.ExecuteCommand(update.Message);
            
            return Ok();
        }
    }
}