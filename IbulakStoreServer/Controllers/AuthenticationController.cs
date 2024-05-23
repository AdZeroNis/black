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
        [HttpPost("Login")]
        public async Task<Results<Ok<AccessTokenResponse>, EmptyHttpResult, ProblemHttpResult>> Login([FromBody] LoginRequestDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider sp)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
            var user = await _userManager.FindByNameAsync(login.Mobile);
            if (user == null)
            {
                return TypedResults.Problem("نام کاربری یا رمز عبور اشتباه است", statusCode: StatusCodes.Status401Unauthorized);
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, isPersistent, lockoutOnFailure: true);
            if (!result.Succeeded)
            {
                return TypedResults.Problem("نام کاربری یا رمز عبور اشتباه است", statusCode: StatusCodes.Status401Unauthorized);
            }
            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return TypedResults.Empty;
        }
    }
}