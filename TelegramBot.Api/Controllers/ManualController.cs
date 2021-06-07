using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Api.Services;

namespace TelegramBot.Api.Controllers
{
    [ApiController]
    public class ManualController : Controller
    {
        private readonly IBotService _bot;

        public ManualController(IBotService bot)
        {
            _bot = bot;
        }

        [HttpPost]
        [Route("api/message/test")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            var msg = new Message
            {
                Text = "savechord Am e:O||---|---|---|h:-||-O-|---|---|g:-||---|-O-|---|d:-||---|-O-|---|A:O||---|---|---|E:O||---|---|---|"
            };

            await _bot.ExecuteCommand(msg);

            return Ok();
        }
    }
}