using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class ProductController : GenericController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        public ProductController(IProductService productService) : base(productService)
        { }

        [AllowAnonymous]
        public override async Task<ActionResult<List<ProductReadDto>>> GetAll(
            [FromQuery] int limit = 20,
            [FromQuery] int offset = 0,
            [FromQuery] string orderBy = "id asc"
        )
        {
            return await base.GetAll(limit, offset, orderBy);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            return await base.GetById(id);
        }

        [Authorize(Policy = "SellerOnly")]
        public override async Task<ActionResult<ProductReadDto>> AddOne(ProductCreateDto dto)
        {
            return await base.AddOne(dto);
        }

    }
}