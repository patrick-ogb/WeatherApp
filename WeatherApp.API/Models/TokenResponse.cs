using System.IdentityModel.Tokens.Jwt;

namespace WeatherApp.API.Models
{
    public class TokenResponse
    {
        public JwtSecurityToken Token { get; set; } = new JwtSecurityToken();
        public string? TokenAsString { get; set; }
    }
}
