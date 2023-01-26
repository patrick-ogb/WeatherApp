using Microsoft.AspNetCore.Identity;

namespace WeatherApp.API.Contexts
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime LastLogin { get; set; }
    }
}
