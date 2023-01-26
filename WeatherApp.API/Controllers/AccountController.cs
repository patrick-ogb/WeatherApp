using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using WeatherApp.API.Contexts;
using WeatherApp.API.services;
using WeatherApp.API.ViewModels;
using WeatherApp.API.ViewModels.Authentication;
using WeatherApp.Shared;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            IUserService userService,
            UserManager<ApplicationUser> userManager
            )
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please, provide all required fields");

            if (!(model.Role == UserRoles.Admin || model.Role == UserRoles.User || model.Role == UserRoles.Tester))
                return BadRequest("Role must be either {Admin, User, Tester}");

            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
                return BadRequest($"User {model.Email} already exists");

            var identityUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (!result.Succeeded)
                return BadRequest("User could not be created!");

            switch (model.Role)
            {
                case "Admin":
                    await _userManager.AddToRoleAsync(identityUser, UserRoles.Admin);
                    break;
                case "Tester":
                    await _userManager.AddToRoleAsync(identityUser, UserRoles.Tester);
                    break;
                default:
                    await _userManager.AddToRoleAsync(identityUser, UserRoles.User);
                    break;
            }

            return Created(nameof(RegisterAsync), $"{model.Role} {model.Email} created");
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Some properties are not valid");

            LoginModel login = new LoginModel
            {
                Email = model.Email,
                Password = model.Password,
            };
            var result = await _userService.LoginAsync(login);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
