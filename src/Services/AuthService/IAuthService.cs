namespace Backend.src.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> LogInAsync (string email, string password);
        Task<User> AuthenticateUserAsync(string token);
        Task SignOutAsync();
    }
}