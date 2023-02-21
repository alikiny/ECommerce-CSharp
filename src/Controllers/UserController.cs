using Backend.src.Models;

namespace Backend.src.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : GenericController<User, UserDto>
    {
        public UserController(IBaseService<User, UserDto> service) : base(service)
        {
        }
    }
}