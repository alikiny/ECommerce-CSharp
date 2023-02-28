using AutoMapper;
using Backend.src.Data;
using Backend.src.Services.BaseService;

namespace Backend.src.Services.ProductService
{
    public class ProductService : BaseService<Product, ProductDto, ProductDto, ProductDto>, IProductService
    {
        public ProductService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}