using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyrlandAAC.Models;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController: Controller
    {
        private readonly OpenTibiaContext _context;
        public PlayerController(OpenTibiaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            var r = _context.Players.ToList();
            return Ok(r);
        }
        

    }
}