using Microsoft.AspNetCore.Mvc;
using QuestEngine.Shared.Dtos.Request;
using QuestEngine.Shared.Dtos.Response;

namespace QuestEngine.API.Controllers
{
    [ApiController]
    public class QuestController : Controller
    {
        [HttpPost(Urls.Quest.QuestProgress)]
        [ProducesResponseType(typeof(QuestProgressResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> QuestProgress([FromBody] QuestProgressRequestDto request)
        {
            throw new NotImplementedException();
        }

        [HttpGet(Urls.Quest.QuestState)]
        [ProducesResponseType(typeof(QuestStateResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> QuestState(string playerId)
        {
            throw new NotImplementedException();
        }
    }
}
