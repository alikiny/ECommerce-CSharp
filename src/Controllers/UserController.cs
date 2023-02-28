using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class UserController : GenericController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _service;
        public UserController(IUserService service) : base(service)
        {
            _service = service;
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

        [AllowAnonymous]
        public new async Task<ActionResult<User>> AddOne(UserCreateDto dto)
        {
            var createdEntity = await _service.AddOneAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.ID }, createdEntity);
        }
    }
}