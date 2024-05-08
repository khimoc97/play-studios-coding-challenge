using Microsoft.AspNetCore.Mvc;
using QuestEngine.Core.Services.Interfaces;
using QuestEngine.Shared.Dtos.Response;
using System.Diagnostics;

namespace QuestEngine.API.Controllers
{
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerService _playerService;

        public PlayerController(ILogger<PlayerController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        [HttpGet(Urls.Player.GetCurrentPlayer)]
        [ProducesResponseType(typeof(PlayerResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentPlayer()
        {
            var sw = Stopwatch.StartNew();
            _logger.LogInformation($"GetCurrentPlayer - Starting get current player");

            var result = await _playerService.GetCurrentPlayerAsync();

            sw.Stop();
            _logger.LogInformation($"GetCurrentPlayer - Completed in {sw.ElapsedMilliseconds}ms");

            return new OkObjectResult(result);
        }
    }
}
