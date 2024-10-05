using Magnum_PPT.Dto;
using Magnum_PPT.Entities;

namespace Magnum_PPT.Services
{
    public interface IGameService
    {
        Task<GameDTO> StartGameAsync(int playerOneId, int playerTwoId);
        Task<RoundDTO> PlayRoundAsync(int gameId, string playerOneMove, string playerTwoMove);
        Task<GameDTO> GetGameStatusAsync(int gameId);
    }
}
