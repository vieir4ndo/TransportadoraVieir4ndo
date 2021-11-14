using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TV.DAL.Entities;
using TV.API.ViewModels;
using Microsoft.Extensions.Options;
using TV.SER.DTOs;
using TV.SER.Interfaces;
using System.Web;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Configuration;

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
        private readonly ICloudStorage _cloudStorage;
        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        public UsersController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<EmailOptionsDTO> emailOptions,
            IEmail email,
            ICloudStorage cloudStorage,
            IMapper mapper,
            IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailOptions = emailOptions;
            _email = email;
            _cloudStorage = cloudStorage;
            _mapper = mapper;
            _config = config;
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

            await SendConfirmationEmail(user, model.Email);

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
            
            await SendConfirmationEmail(user, model.Email);

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
            
            await SendConfirmationEmail(user, model.Email);

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
            
            await SendConfirmationEmail(user, model.Email);

            var userFromDb  = await _userManager.FindByNameAsync(user.UserName);
            await _userManager.AddToRoleAsync(userFromDb, "ShipmentWorker");

            return Ok(result);
        }

        private async Task SendConfirmationEmail(User user, string email)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmEmailUrl = _config.GetSection("Urls:ConfirmEmail").Value;

            var uriBuilder = new UriBuilder(confirmEmailUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["token"] = token;
            query["userid"] = user.Id;
            uriBuilder.Query = query.ToString();
            var urlString = uriBuilder.ToString();

            var emailBody = $"Please confirm your email by clicking <a href='{urlString}'>here</a></br>";
            await _email.Send(email, emailBody, _emailOptions.Value);
        }

        [HttpPut("update-client/{id}")]
        [Authorize(Policy = "ClientPolicy")]
        public async Task<IActionResult> UpdateClient(string id, [FromForm] UpdateUserViewModel model)
        {
            return await UpdateUser(id, model);
        }

        [HttpPut("update-administrator/{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> UpdateAdministrator(string id, [FromForm] UpdateUserViewModel model)
        {
            return await UpdateUser(id, model);
        }

        [HttpPut("update-deliveryworker/{id}")]
        [Authorize(Policy = "DeliveryworkerPolicy")]
        public async Task<IActionResult> UpdateDeliveryworker(string id, [FromForm] UpdateUserViewModel model)
        {
            return await UpdateUser(id, model);
        }

        [HttpPut("update-shipmentWorker/{id}")]
        [Authorize(Policy = "ShipmentWorkerPolicy")]
        public async Task<IActionResult> UpdateShipmentWorker(string id, [FromForm] UpdateUserViewModel model)
        {
            return await UpdateUser(id, model);
        }

        private async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserViewModel model)
        {
            if (id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            var userDb = await _userManager.FindByIdAsync(id);

            if (!String.IsNullOrEmpty(model.Email)){
                userDb.Email= model.Email;
                await SendConfirmationEmail(userDb, model.Email);
                userDb.EmailConfirmed = false;
            }

            if (!String.IsNullOrEmpty(model.FederalRegistration))
                userDb.FederalRegistration= model.FederalRegistration;

            if (!String.IsNullOrEmpty(model.Name))
                userDb.Name= model.Name;

            if (!String.IsNullOrEmpty(model.Adress))
                userDb.Adress= model.Adress;

            if (model.ProfileImage != null){
                if (userDb.ProfileImageUrl != null)
                {
                    await _cloudStorage.DeleteImage(userDb.ProfileImageUrl);
                }
                var addedFileNameUrl = await _cloudStorage.UploadAsync(model.ProfileImage);
                userDb.ProfileImageUrl = addedFileNameUrl;
            }

            var result = await _userManager.UpdateAsync(userDb);
            var updatedUser = await _userManager.FindByIdAsync(id);
            var userToReturn = _mapper.Map<UserViewModel>(updatedUser);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = result,
                    user = userToReturn
                });
            }

            return BadRequest(result);
        }

    }
}
