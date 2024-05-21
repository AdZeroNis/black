using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Authentication;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly StoreDbContext _context;
        public AuthenticationController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,

            StoreDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Mobile);
            if (user != null)
            {
                return BadRequest("با این شماره همراه قبلا ثبت نام انجام شده است");
            }
            user = new AppUser
            {
                UserName = model.Mobile,
                PhoneNumber = model.Mobile,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }
    }
}