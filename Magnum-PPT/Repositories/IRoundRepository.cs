using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public interface IRoundRepository
    {
        Task AddRoundAsync(Round round);
        Task SaveChangesAsync();
    }
}
