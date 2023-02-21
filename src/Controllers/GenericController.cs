using Backend.src.Services.BaseService;

namespace Backend.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class GenericController<TEntity, TDto> : ControllerBase
    where TEntity:BaseModel
    {
        private readonly IBaseService<TEntity, TDto> _service;

        public GenericController(IBaseService<TEntity, TDto> service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<TEntity>>> GetAll(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] string orderBy = "id asc"
        )
        {
            try
            {
                var response = Ok(await _service.GetAllAsync(orderBy, limit, offset));
                return response;
            }
            catch (ServiceException ex)
            {
                return ErrorHandler(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return response;
            }
            catch (ServiceException ex)
            {
                return ErrorHandler(ex);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TEntity>> UpdateOne(int id, TDto update)
        {
            try
            {
                var response = await _service.UpdateOneAsync(id, update);
                return response;
            }
            catch (ServiceException ex)
            {
                return ErrorHandler(ex);
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<TDto>> AddOne(TDto newOne)
        {
            try
            {
                var createdEntity = await _service.AddOneAsync(newOne);
                return CreatedAtAction(nameof(GetById), new{id = createdEntity.ID}, createdEntity);
            }
            catch (ServiceException ex)
            {
                return ErrorHandler(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteById(int id)
        {
            try
            {
                var response = await _service.DeleteByIdAsync(id);
                return response;
            }
            catch (ServiceException ex)
            {
                return ErrorHandler(ex);
            }
        }

        private ActionResult ErrorHandler(ServiceException ex)
        {
            switch (ex.HttpStatusCode)
            {
                case 400:
                    return BadRequest(ex.Message);
                case 401:
                    return Unauthorized(ex.Message);
                case 404:
                    return NotFound(ex.Message);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}