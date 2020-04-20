using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyrlandAAC.Models;
using MyrlandAAC.Services;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController: Controller
    {
        private readonly IPlayerService _pService;
        public PlayerController(IPlayerService pService)
        {
            _pService = pService;
        }

        [HttpGet, Route("search/{name}")]
        public async Task<IActionResult> SearchByName(string name) {
            var r = await _pService.SearchPlayer(name);
            return Ok(r);
        }
        

    }
}