using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public interface IGameRepository
    {
        Task<Game> GetGameByIdAsync(int gameId);
        Task AddGameAsync(Game game);
        Task SaveChangesAsync();
    }
}
