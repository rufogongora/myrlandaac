using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyrlandAAC.Services;
using MyrlandAAC.ViewModels;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(
            IAuthService authService,
            IMapper mapper) {
            _authService = authService;
            _mapper = mapper;
        }
        
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginViewModel login) {
            var res =  await _authService.Login(login);
            if (res != null) {
                return Ok(res);
            } else {
                return BadRequest(new { Error = "Invalid username or password." });
            }
        }

        [HttpPost, Route("createaccount")]
        public async Task<IActionResult> CreateAccount(LoginViewModel login) {
            var acc =  await _authService.CreateAccount(login);
            var res = _mapper.Map<AccountViewModel>(acc);
            if (res != null) {
                return Ok(res);
            } else {
                return BadRequest(new { Error = "Account already exists." });
            }
        }

        [Authorize]
        [HttpGet, Route("me")]
        public IActionResult Me() {
            return Ok(User.Identity.Name);
        }   
    }
}