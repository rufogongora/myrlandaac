using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.Models;
using MyrlandAAC.ViewModels;

namespace MyrlandAAC.Services
{
    public interface IAuthService {
        Task<Account> Login(LoginViewModel login);
        Task<Account> CreateAccount(LoginViewModel login);
    }
    public class AuthService: IAuthService
    {
        private readonly OpenTibiaContext _context;
        public AuthService(OpenTibiaContext context) {
            _context = context;
        }

        public async Task<Account> Login(LoginViewModel login) {

            var sha1Password = ToSha1(login.Password);

            var acc = await _context.Accounts.Where(x => 
                x.Name == login.Username && 
                x.Password == sha1Password)
                .FirstOrDefaultAsync();

            return acc;
        }

        public async Task<Account> CreateAccount(LoginViewModel login) {
            
            var existing = await _context
                .Accounts
                .Where(x => x.Name == login.Username)
                .FirstOrDefaultAsync();

            if (existing != null) {
                return null;
            }

            var acc = new Account {
                Name = login.Username.ToLower(),
                Password = ToSha1(login.Password)
            };

            _context.Add(acc);
            await _context.SaveChangesAsync();

            return acc;
        }

        private string ToSha1(string str) {
            using (SHA1Managed sha1 = new SHA1Managed()) {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash) {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}