using Backend.src.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class UserController : GenericController<User, UserDto>
    {
        public UserController(IUserService service) : base(service)
        {
        }

        [Authorize(Policy = "AdminOnly")]
        public override async Task<ActionResult<List<User>>> GetAll(
                [FromQuery] int limit = 20,
                [FromQuery] int offset = 0,
                [FromQuery] string orderBy = "id asc"
            )
        {
            return await base.GetAll(limit, offset, orderBy);
        }
    }
}