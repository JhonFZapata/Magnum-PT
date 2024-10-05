using Magnum_PPT.Dto;
using Magnum_PPT.Repositories;
using Magnum_PPT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_PPT.Controllers
{
    // Controlador para manejar el juego
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;

        public GameController(IGameService gameService, IPlayerService playerService)
        {
            _gameService = gameService;
            _playerService = playerService;
        }

        // Endpoint para manejar el inicio de un juego       
        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] StartGameDTO startGameDto)
        {
            try
            {
                var player = await _playerService.GetPlayerByIdAsync(startGameDto.PlayerOneId);
                if (player == null) 
                {
                    return NotFound(new { message = "jugadores no existen" });
                }
                // Llamada al servicio
                var game = await _gameService.StartGameAsync(startGameDto.PlayerOneId, startGameDto.PlayerTwoId);

                // Validar existencia del juego
                if (game == null)
                {
                    return NotFound(new { message = "No se pudo iniciar el juego." });
                }

                // Retornar juego
                return Ok(game); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al iniciar el juego.", error = ex.Message });
            }
        }

        // Endpoint para manejar las rondas
        [HttpPost("{gameId}/play-round")]
        public async Task<IActionResult> PlayRound(int gameId, [FromBody] PlayRoundDTO playRoundDto)
        {

            if (playRoundDto == null)
            {
                return BadRequest("Cuerpo de solicitud invalida o inexistente.");
            }

            var round = await _gameService.PlayRoundAsync(gameId, playRoundDto.PlayerOneMove, playRoundDto.PlayerTwoMove);
            return Ok(round);
        }

        // Endpoint para obtener el estado de un juego
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameStatus(int gameId)
        {
            var game = await _gameService.GetGameStatusAsync(gameId);
            return Ok(game);
        }
    }
}
