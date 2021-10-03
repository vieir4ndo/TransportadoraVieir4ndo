using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TV.DAL.Entities;
using TV.API.ViewModels;

namespace TV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministratorsController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateAdministratorsViewModel model)
        {
            if(!(await _roleManager.RoleExistsAsync("Administrator")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "Administrator");

            return Ok(result);
        }

    }
}
