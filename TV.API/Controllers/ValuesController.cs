using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV.API.ViewModels;
using TV.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContex _context;


        public ValuesController( ApplicationDbContex context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Policy = "ClientPolicy")]
        public async Task<IActionResult> Values()
        {
            var values = await _context.Values.ToListAsync(); 
            return Ok(values);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ValueById(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id); 
            return Ok(value);
        }
    }
}
