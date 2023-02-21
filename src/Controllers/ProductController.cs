namespace Backend.src.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : GenericController<Product, ProductDto>
    {
        public ProductsController(IProductService productService): base(productService)
        { }
    }
}