using System.Security.AccessControl;
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
using Microsoft.Extensions.Options;
using TV.SER.DTOs;
using TV.SER.Interfaces;
using System.Web;

namespace TV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<EmailOptionsDTO> _emailOptions;
        private readonly IEmail _email;

        public UsersController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<EmailOptionsDTO> emailOptions,
            IEmail email)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailOptions = emailOptions;
            _email = email;
        }

        [HttpPost("create-administrator")]
        public async Task<IActionResult> CreateAdministrator(CreateUsersViewModel model)
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

            await SendConfirmationEmail(user, model);

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "Administrator");

            return Ok(result);
        }

        [HttpPost("create-client")]
        public async Task<IActionResult> CreateClient(CreateUsersViewModel model)
        {
            if(!(await _roleManager.RoleExistsAsync("Client")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Client"));
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
            
            await SendConfirmationEmail(user, model);

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "Client");

            return Ok(result);
        }

        [HttpPost("create-deliveryworker")]
        public async Task<IActionResult> CreateDeliveryWorker(CreateUsersViewModel model)
        {
            if(!(await _roleManager.RoleExistsAsync("DeliveryWorker")))
            {
                await _roleManager.CreateAsync(new IdentityRole("DeliveryWorker"));
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
            
            await SendConfirmationEmail(user, model);

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "DeliveryWorker");

            return Ok(result);
        }

        [HttpPost("create-shipmentWorker")]
        public async Task<IActionResult> CreateShipmentWorker(CreateUsersViewModel model)
        {
            if(!(await _roleManager.RoleExistsAsync("ShipmentWorker")))
            {
                await _roleManager.CreateAsync(new IdentityRole("ShipmentWorker"));
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
            
            await SendConfirmationEmail(user, model);

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "ShipmentWorker");

            return Ok(result);
        }

        private async Task SendConfirmationEmail(User user, CreateUsersViewModel model)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmEmailUrl = Request.Headers["confirmEmailUrl"];//http://localhost:4200/email-confirm

            var uriBuilder = new UriBuilder(confirmEmailUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = user.Id;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            var emailBody = $"Please confirm your email by clicking on the link below </br>{urlString}";
            await _email.Send(model.Email, emailBody, _emailOptions.Value);
        }

    }
}
