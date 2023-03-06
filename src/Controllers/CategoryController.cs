using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    [Route("api/v1/categories")]
    [Authorize(Policy = "AdminOnlyPolicy")]
    public class CategoryController : GenericController<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        public CategoryController(ICategoryService service) : base(service)
        {
        }

        [AllowAnonymous]
        public override async Task<ActionResult<List<CategoryReadDto>>> GetAll([FromQuery] GetAllQueryOptions options)
        {
            return await base.GetAll(options);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<CategoryReadDto>> GetById(int id)
        {
            return await base.GetById(id);
        }
    }
}