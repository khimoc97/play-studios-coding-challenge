using Microsoft.AspNetCore.Mvc;
using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Shared.Dtos.Request;
using QuestEngine.Shared.Dtos.Response;
using System.Diagnostics;

namespace QuestEngine.API.Controllers
{
    [ApiController]
    public class QuestController : Controller
    {
        private readonly ILogger<QuestController> _logger;
        private readonly IQuestService _questService;

        public QuestController(ILogger<QuestController> logger, IQuestService questService)
        {
            _logger = logger;
            _questService = questService;
        }

        [HttpPost(Urls.Quest.QuestProgress)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuestProgressResponseDto))]
        public async Task<IActionResult> UpdateQuestProgress([FromBody] QuestProgressRequestDto request)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"UpdateQuestProgress - Player's ID: {request.PlayerId} starting update quest progress");

            var result = await _questService.UpdateQuestProgressAsync(request);

            sw.Stop();
            _logger.LogInformation($"UpdateQuestProgress - Player's ID: {request.PlayerId} completed in {sw.ElapsedMilliseconds}ms");

            return new OkObjectResult(result);
        }

        [HttpGet(Urls.Quest.QuestState)]
        [ProducesResponseType(typeof(QuestStateResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> QuestState(string playerId)
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"QuestState - Player's ID: {playerId} starting get quest state");

            var result = await _questService.GetQuestStateByPlayerIdAsync(playerId);

            sw.Stop();
            _logger.LogInformation($"QuestState - Player's ID: {playerId} completed in {sw.ElapsedMilliseconds}ms");

            return new OkObjectResult(result);
        }
    }
}
