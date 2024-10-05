using Magnum_PPT.Db;
using Magnum_PPT.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Magnum_PPT.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDbContext _context;

        public PlayerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayerByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        // Método para obtener un jugador por nombre
        public async Task<Player> GetPlayerByNameAsync(string name)
        {
            return await _context.Players.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task AddPlayerAsync(Player player)
        {
            await _context.Players.AddAsync(player);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
