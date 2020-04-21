using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.Models;

namespace MyrlandAAC.Services
{
    public interface IPlayerService {
        Task<IEnumerable<Player>> SearchPlayer(string name);
        Task<Player> CreatePlayer(
            string accountName,
            string playerName,
            int vocation);
    }
    public class PlayerService : IPlayerService
    {
        private readonly OpenTibiaContext _context;
        public PlayerService(
            OpenTibiaContext context
        ) {
            _context = context;
        }
        public async Task<IEnumerable<Player>> SearchPlayer(string name) {
            var res = await _context
                .Players
                .Where(x => x.Name.Contains(name))
                .ToListAsync();
            return res;
        }

        public async Task<Player> CreatePlayer(
            string accountName,
            string playerName,
            int vocation) {
            
            var acc = await _context
                .Accounts
                .Where(x => x.Name == accountName)
                .FirstOrDefaultAsync();

            if (acc == null) {
                return null;
            }

            var exists = await _context
                .Players
                .Where(p => p.Name == playerName)
                .CountAsync();
            
            if (exists > 0) {
                return null;
            }    

            var player = new Player { 
                AccountId = acc.Id,
                Name = playerName,
                Vocation = vocation
            };

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return player;
        }
    }
}