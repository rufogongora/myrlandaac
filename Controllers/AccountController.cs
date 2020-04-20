using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyrlandAAC.Services;
using MyrlandAAC.ViewModels;
using MyrlandAAC.Models;

namespace MyrlandAAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accService;
        private readonly IMapper _mapper;

        public AccountController(
            IMapper mapper,
            IAccountService accService
            )
        {
            _mapper = mapper;
            _accService = accService;
        }

        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var res = await _accService.GetAccount(id);
            var view = _mapper.Map<AccountViewModel>(res);
            return Ok(view);
        }
    }
}