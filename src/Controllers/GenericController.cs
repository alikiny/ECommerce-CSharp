using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class GenericController<TEntity, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
        where TEntity : BaseModel
    {
        protected readonly IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> _service;

        public GenericController(IBaseService<TEntity, TReadDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet("")]
        public virtual async Task<ActionResult<List<TReadDto>>> GetAll(
            [FromQuery] GetAllQueryOptions options
        )
        {
            var response = Ok(await _service.GetAllAsync(options));
            return response;
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TReadDto>> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TReadDto>> UpdateOne(int id, TUpdateDto update)
        {
            var response = await _service.UpdateOneAsync(id, update);
            return Ok(response);
        }

        [HttpPost("")]
        public virtual async Task<ActionResult<TReadDto>> AddOne(TCreateDto dto)
        {
            var createdEntity = await _service.AddOneAsync(dto);
            return CreatedAtAction(nameof(AddOne), createdEntity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<bool>> DeleteById(int id)
        {
            var response = await _service.DeleteByIdAsync(id);
            return response;
        }

       /*  private ActionResult ErrorHandler(ServiceException ex)
        {
            switch (ex.HttpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(ex.Message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized(ex.Message);
                case HttpStatusCode.NotFound:
                    return NotFound(ex.Message);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        } */
    }
}
