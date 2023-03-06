using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    public class ProductController : GenericController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        public ProductController(IProductService productService) : base(productService)
        { }

        [AllowAnonymous]
        public override async Task<ActionResult<List<ProductReadDto>>> GetAll([FromQuery] GetAllQueryOptions options)
        {
            return await base.GetAll(options);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<ProductReadDto>> GetById(int id)
        {
            return await base.GetById(id);
        }

        [Authorize(Policy = "SellerOnlyPolicy")]
        public override async Task<ActionResult<ProductReadDto>> AddOne(ProductCreateDto dto)
        {
            return await base.AddOne(dto);
        }

        [Authorize(Policy = "ProductDeletePolicy")]
        public override async Task<ActionResult<ProductReadDto>> UpdateOne(int id, ProductUpdateDto update)
        {
            return await base.UpdateOne(id, update);
        }
    }
}