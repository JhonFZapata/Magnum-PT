using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerByIdAsync(int id);
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task AddPlayerAsync(Player player);
        Task SaveChangesAsync();
    }
}
