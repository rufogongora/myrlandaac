using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.ViewModels;
using MyrlandAAC.Models;

namespace MyrlandAAC.Services
{
    public interface IAccountService {
        Account FindAccount(string name);
        Task<Account> GetAccount(int id);
    }
    public class AccountService : IAccountService
    {
        private readonly OpenTibiaContext _context;
        public AccountService(
            OpenTibiaContext context
        ) {
            _context = context;
        }
        public Account FindAccount(string name) {
            var res = _context
                .Accounts
                .Include(x => x.Players)
                .Where(x => x.Name == name)
                .FirstOrDefault();
            
            return res;     
        }

        public async Task<Account> GetAccount(int id) {
            var res = await _context
                .Accounts
                .Include(x => x.Players)
                .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
            
            return res;     
        }
    }
}