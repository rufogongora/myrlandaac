using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyrlandAAC.Models;
using MyrlandAAC.Services;
using MyrlandAAC.ViewModels;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController: Controller
    {
        private readonly IPlayerService _pService;
        private readonly IMapper _mapper;
        public PlayerController(
            IPlayerService pService,
            IMapper mapper)
        {
            _pService = pService;
            _mapper = mapper;
        }

        [HttpGet, Route("search/{name}")]
        public async Task<IActionResult> SearchByName(string name) {
            var r = await _pService.SearchPlayer(name);
            return Ok(r);
        }

        [Authorize]
        [HttpPost, Route("createcharacter")]
        public async Task<IActionResult> CreateCharacter([FromBody] PlayerViewModel player) {
            if (!ModelState.IsValid) {
                return BadRequest("WAAA");
            }
            var res = await _pService.CreatePlayer(
                User.Identity.Name,
                player.Name,
                player.Vocation);

            if (res == null) {
                return BadRequest("WEY NOOO");
            }
            return Ok(_mapper.Map<PlayerViewModel>(res));
        }
        

    }
}