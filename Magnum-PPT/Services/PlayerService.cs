using AutoMapper;
using Magnum_PPT.Dto;
using Magnum_PPT.Entities;
using Magnum_PPT.Repositories;

namespace Magnum_PPT.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        // Metodo para registrar un jugador
        public async Task<PlayerDTO> RegisterPlayerAsync(PlayerDTO playerDto)
        {
            // Validación 
            if (string.IsNullOrWhiteSpace(playerDto.Name))
            {
                throw new ArgumentException("El nombre del jugador es requerido.");
            }

            // Validación de nombre existente
            var existingPlayers = await _playerRepository.GetAllPlayersAsync();
            if (existingPlayers.Any(p => p.Name == playerDto.Name))
            {
                throw new ArgumentException("El nombre jugador ya está registrado.");
            }

            // mapear y retornar
            var player = _mapper.Map<Player>(playerDto);
            await _playerRepository.AddPlayerAsync(player);
            await _playerRepository.SaveChangesAsync();
            return _mapper.Map<PlayerDTO>(player);
        }

        // Metodo para obtener un jugador por Id
        public async Task<PlayerDTO> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetPlayerByIdAsync(id);
            return _mapper.Map<PlayerDTO>(player);
        }


        // Metodo para devolver todos los juadores
        public async Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllPlayersAsync();
            return _mapper.Map<IEnumerable<PlayerDTO>>(players);
        }
    }
}
