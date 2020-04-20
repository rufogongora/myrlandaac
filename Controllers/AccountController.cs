using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.Models;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly OpenTibiaContext _context;
        public AccountController(OpenTibiaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _context.Accounts
                    .Include(x => x.Players).OrderBy(x => x.Id).ToList();
            return Ok(res);
        }
    }
}