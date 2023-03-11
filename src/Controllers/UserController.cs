using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class UserController : GenericController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        public UserController(IUserService service) : base(service)
        {
        }

        [Authorize(Policy = "AdminOnlyPolicy")]
        public override async Task<ActionResult<List<UserReadDto>>> GetAll([FromQuery] GetAllQueryOptions options)
        {
            return await base.GetAll(options);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto>> AddOne(UserCreateDto dto)
        {
            var createdEntity = await _service.AddOneAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.ID }, createdEntity);
        }
    }
}