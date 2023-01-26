using WeatherApp.Shared;

namespace WeatherApp.API.services
{
    public interface IUserService
    {
        Task<UserManagerResponse> LoginAsync(LoginModel model);
    }
}
