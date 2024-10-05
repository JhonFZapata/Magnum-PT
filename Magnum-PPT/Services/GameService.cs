using AutoMapper;
using Magnum_PPT.Dto;
using Magnum_PPT.Entities;
using Magnum_PPT.Repositories;

namespace Magnum_PPT.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IRoundRepository _roundRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IRoundRepository roundRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
            _mapper = mapper;
        }

        // Método para iniciar un nuevo juego
        public async Task<GameDTO> StartGameAsync(int playerOneId, int playerTwoId)
        {
            var game = new Game
            {
                PlayerOneId = playerOneId,
                PlayerTwoId = playerTwoId,
                IsFinished = false,
                Rounds = new List<Round>()
            };

            await _gameRepository.AddGameAsync(game);
            await _gameRepository.SaveChangesAsync();

            return _mapper.Map<GameDTO>(game);
        }

        // Método para registrar movimientos de una ronda
        public async Task<RoundDTO> PlayRoundAsync(int gameId, string playerOneMove, string playerTwoMove)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameId);

            if (game == null || game.IsFinished)
            {
                throw new ArgumentException("El juego no existe o ya ha terminado.");
            }

            // Crear una ronda
            var round = new Round
            {
                GameId = gameId,
                PlayerOneMove = playerOneMove,
                PlayerTwoMove = playerTwoMove,
                WinnerPlayerId = DetermineRoundWinner(playerOneMove, playerTwoMove, game.PlayerOneId, game.PlayerTwoId),
                CreatedAt = DateTime.UtcNow
            };

            await _roundRepository.AddRoundAsync(round);
            await _roundRepository.SaveChangesAsync();

            // Verificar si alguno de los jugadores ha ganado 3 rondas
            int playerOneWins = game.Rounds.Count(r => r.WinnerPlayerId == game.PlayerOneId);
            int playerTwoWins = game.Rounds.Count(r => r.WinnerPlayerId == game.PlayerTwoId);

            if (playerOneWins == 3)
            {
                game.IsFinished = true;
                game.WinnerPlayerId = game.PlayerOneId;
            }
            else if (playerTwoWins == 3)
            {
                game.IsFinished = true;
                game.WinnerPlayerId = game.PlayerTwoId;
            }

            await _gameRepository.SaveChangesAsync();

            return _mapper.Map<RoundDTO>(round);
        }

        // Método para determinar el ganador una ronda
        private int? DetermineRoundWinner(string playerOneMove, string playerTwoMove, int playerOneId, int playerTwoId)
        {
            if (playerOneMove == playerTwoMove)
            {
                // Empate
                return null;
            }

            // Comparar movimientos
            if ((playerOneMove == "Piedra" && playerTwoMove == "Tijera") ||
                (playerOneMove == "Papel" && playerTwoMove == "Piedra") ||
                (playerOneMove == "Tijera" && playerTwoMove == "Papel"))
            {
                return playerOneId;
            }

            return playerTwoId;
        }

        // Método para obtener el estado de un juego
        public async Task<GameDTO> GetGameStatusAsync(int gameId)
        {
            var game = await _gameRepository.GetGameByIdAsync(gameId);
            return _mapper.Map<GameDTO>(game);
        }
    }
}
