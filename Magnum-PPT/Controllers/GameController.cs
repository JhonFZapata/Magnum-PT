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
                //var player = await _playerService.GetPlayerByIdAsync(startGameDto.PlayerOneId);
                var playerOne = await _playerService.GetPlayerByNameAsync(startGameDto.PlayerOneName);
                var playerTwo = await _playerService.GetPlayerByNameAsync(startGameDto.PlayerTwoName);
                if (playerOne == null || playerTwo == null) 
                {
                    return NotFound(new { message = "uno o ambos jugadores no existen" });
                }
                // Llamada al servicio
                var game = await _gameService.StartGameAsync(playerOne.Id, playerTwo.Id);

                // Validar existencia del juego
                if (game == null)
                {
                    return NotFound(new { message = "No se pudo iniciar el juego." });
                }

                Console.WriteLine($"Jugador 1: {playerOne.Name}, Jugador 2: {playerTwo.Name}");

                var response = new
                {
                    Game = game,
                    PlayerOneName = playerOne.Name,
                    PlayerTwoName = playerTwo.Name
                };

                // Retornar juego y nombres
                return Ok(response);
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
