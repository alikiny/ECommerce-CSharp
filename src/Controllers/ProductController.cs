using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class ProductController : GenericController<Product, ProductDto, ProductDto, ProductDto>
    {
        public ProductController(IProductService productService) : base(productService)
        { }

        [AllowAnonymous]
        public override async Task<ActionResult<List<ProductDto>>> GetAll(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] string orderBy = "id asc"
        )
        {
            return await base.GetAll(limit, offset, orderBy);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<ProductDto>> GetById(int id)
        {
            return await base.GetById(id);
        }
    }
}