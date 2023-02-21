namespace Backend.src.Services.UserService
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        public UserService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}