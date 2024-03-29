using Backend.src.Services.BaseService;

namespace Backend.src.Services.ProductService
{
    public interface IProductService: IBaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
         Task<ProductReadDto> AddOneAsync(ProductCreateDto dto, int sellerId);
    }
}