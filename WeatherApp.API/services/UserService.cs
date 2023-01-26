using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherApp.API.Contexts;
using WeatherApp.API.Models;
using WeatherApp.Shared;

namespace WeatherApp.API.services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _configuration;
        public UserService(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManger = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<UserManagerResponse> LoginAsync(LoginModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = $"There is no user with that Email:{model.Email}",
                    IsSuccess = false,
                };
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };


            var roles = await _userManger.GetRolesAsync(user);

            var token = TokenString(user, roles, model.Email!, model.Password!);

            return new UserManagerResponse
            {
                Message = token.TokenAsString,
                IsSuccess = true,
                ExpireDate = token.Token.ValidTo
            };
        }

        private TokenResponse TokenString(ApplicationUser user, IList<string> roles, string email, string password)
        {
            var claims = new List<Claim>
                    {
                         new Claim("Email", email!),
                         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return  new TokenResponse
            {
                TokenAsString = tokenAsString,
                Token = token
            };
            
        }
    }
}
