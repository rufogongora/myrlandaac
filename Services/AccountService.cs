using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myrlandaac.ViewModels;
using MyrlandAAC.Models;

namespace myrlandaac.Services
{
    public interface IAccountService {
        Account FindAccount(string name);
        Account GetAccount(int id);
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
            .Where(x => x.Name == name)
            .Include(x => x.Players)
            .FirstOrDefault();
            
            return res;     
        }

        public Account GetAccount(int id) {
            var res = _context.Accounts.Where(x => x.Id == id)
            .FirstOrDefault();
            
            return res;     
        }
    }
}