using Microsoft.EntityFrameworkCore;
using Magnum_PPT.Db;
using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public class RoundRepository :IRoundRepository
    {
        private readonly AppDbContext _context;

        public RoundRepository(AppDbContext context)
        {
            _context = context;
        }

        // Crear una ronda
        public async Task AddRoundAsync(Round round)
        {
            await _context.Rounds.AddAsync(round);
        }

        // Guardar cambios en base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
