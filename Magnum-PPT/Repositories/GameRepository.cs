using Microsoft.EntityFrameworkCore;
using Magnum_PPT.Db;
using Magnum_PPT.Entities;

namespace Magnum_PPT.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        // Obtener juego por su Id y las rondas relacionadas
        public async Task<Game> GetGameByIdAsync(int gameId)
        {
            return await _context.Games
                .Include(g => g.Rounds)  
                .FirstOrDefaultAsync(g => g.Id == gameId);
        }

        // Crear un juego
        public async Task AddGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        // Guardar cambios en base de datos
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
