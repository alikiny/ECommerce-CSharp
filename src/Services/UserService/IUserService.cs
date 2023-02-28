using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Backend.src.Services.UserService
{
    public interface IUserService : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
    }
}