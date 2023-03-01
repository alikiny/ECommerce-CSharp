namespace Backend.src.Services.ProductService
{
    public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        public ProductService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}