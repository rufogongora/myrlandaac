using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyrlandAAC.Enums;
using MyrlandAAC.Models;
using MyrlandAAC.ViewModels;

namespace MyrlandAAC.Services
{
    public interface IAuthService {
        Task<LoginResponseViewModel> Login(LoginViewModel login);
        Task<RegisterResponseViewModel> CreateAccount(LoginViewModel login);
    }
    public class AuthService: IAuthService
    {
        private readonly OpenTibiaContext _context;
        private readonly string _secret;
        private readonly IMapper _mapper;
        public AuthService(
            OpenTibiaContext context,
            IConfiguration configuration,
            IMapper mapper) {
            _secret = configuration.GetSection("JwtConfig").GetSection("secret").Value;
            _mapper = mapper;
            _context = context;
        }

        public async Task<LoginResponseViewModel> Login(LoginViewModel login) {

            var sha1Password = ToSha1(login.Password);

            var acc = await _context.Accounts.Where(x => 
                    x.Name == login.Username && 
                    x.Password == sha1Password)
                .Include(x => x.Players)
                .FirstOrDefaultAsync();
            
            if (acc == null) {
                return new LoginResponseViewModel {
                    Response = LoginResponseEnum.WrongUsernameOrPassword
                };
            }


            return new LoginResponseViewModel
                { 
                    Account = _mapper.Map<AccountViewModel>(acc), 
                    Token = SignJWTToken(acc),
                    Response = LoginResponseEnum.Success,
                    Role = (RoleEnum)acc.Type
                };
        }

        public async Task<RegisterResponseViewModel> CreateAccount(LoginViewModel login) {
            
            var existing = await _context
                .Accounts
                .Where(x => x.Name == login.Username)
                .FirstOrDefaultAsync();

            if (existing != null) {
                return new RegisterResponseViewModel {
                    Response = RegisterAccountResponseEnum.AccountAlreadyExists
                };
            }

            var acc = new Account {
                Name = login.Username.ToLower(),
                Password = ToSha1(login.Password),
                Type = 1
            };

            _context.Add(acc);
            await _context.SaveChangesAsync();

            return new RegisterResponseViewModel {
                Account = _mapper.Map<AccountViewModel>(acc),
                Response = RegisterAccountResponseEnum.Success
            };
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

        private string SignJWTToken(Account acc) {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, acc.Name),
                    // new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleEnum), acc.Type - 1))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}