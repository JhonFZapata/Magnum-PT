using Magnum_PPT.Dto;

namespace Magnum_PPT.Services
{
    public interface IPlayerService
    {
        Task<PlayerDTO> RegisterPlayerAsync(PlayerDTO playerDto);
        Task<PlayerDTO> GetPlayerByIdAsync(int id);
        Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync();
    }
}
