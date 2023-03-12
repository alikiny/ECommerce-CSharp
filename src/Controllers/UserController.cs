using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.Controllers
{
    public class UserController : GenericController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        public new IUserService _service { get; set; }

        public UserController(IUserService service)
            : base(service)
        {
            _service = service;
        }

        [Authorize(Policy = "AdminOnlyPolicy")]
        public override async Task<ActionResult<List<UserReadDto>>> GetAll(
            [FromQuery] GetAllQueryOptions options
        )
        {
            return await base.GetAll(options);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<UserReadDto>> AddOne(UserCreateDto dto)
        {
            var createdEntity = await _service.AddOneAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.ID }, createdEntity);
        }

        [Authorize(Policy = "UserUpdatePolicy")]
        public override async Task<ActionResult<UserReadDto>> UpdateOne(int id, UserUpdateDto dto)
        {
            return await base.UpdateOne(id, dto);
        }

        [HttpPost("{id:int}/change-password")]
        [Authorize(Policy = "UserUpdatePolicy")]
        public async Task<ActionResult> UpdatePassword(
            [FromRoute] int id,
            [FromBody] string newPassword
        )
        {
            await _service.UpdatePasswordAsync(id, newPassword);
            return Accepted();
        }
    }
}
