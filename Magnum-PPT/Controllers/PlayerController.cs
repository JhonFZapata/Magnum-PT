using Magnum_PPT.Dto;
using Magnum_PPT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_PPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        private readonly IPlayerService _playerService;


        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // Endpoint para registrar un jugador
        [HttpPost("register")]
        public async Task<IActionResult> RegisterPlayer([FromBody] PlayerDTO playerDto)
        {
            try
            {
                // Validar si existe el jugador
                var playerExist = await _playerService.GetPlayerByIdAsync(playerDto.Id);

                if (playerExist == null)
                {
                    return BadRequest("Jugador no existente");
                }

                // Registrar jugador
                var registeredPlayer = await _playerService.RegisterPlayerAsync(playerDto);

                // retornar la respuesta
                return CreatedAtAction(nameof(GetPlayerByNameAsync), new { id = registeredPlayer.Id }, registeredPlayer);
            }
            catch (ArgumentException ex)
            {
                // Manejar errores de validación
                return BadRequest(new { message = ex.Message });
            }
        }

        // Endpoint para obtener un jugador por su Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerByNameAsync(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }
    }
}
