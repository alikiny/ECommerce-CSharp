using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    [Route("api/v1/categories")]
    public class CategoryController : GenericController<Category, CategoryDto,CategoryDto,CategoryDto>
    {
        public CategoryController(ICategoryService service) : base(service)
        {
        }

        [AllowAnonymous]
        public override async Task<ActionResult<List<CategoryDto>>> GetAll(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] string orderBy = "id asc"
        )
        {
            return await base.GetAll(limit, offset, orderBy);
        }
    }
}