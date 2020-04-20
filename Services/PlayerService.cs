using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.Models;

namespace MyrlandAAC.Services
{
    public interface IPlayerService {
        Task<IEnumerable<Player>> SearchPlayer(string name);
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
    }
}