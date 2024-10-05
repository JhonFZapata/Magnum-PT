using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerByIdAsync(int id);
        Task<Player> GetPlayerByNameAsync(string name);
        Task<IEnumerable<Player>> GetAllPlayersAsync();
        Task AddPlayerAsync(Player player);
        Task SaveChangesAsync();
    }
}
