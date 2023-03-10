namespace Backend.src.Services.ProductService
{
    public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        public ProductService(IMapper mapper, IProductRepository repository) : base(mapper, repository)
        {
        }

        public async Task<ProductReadDto> AddOneAsync(ProductCreateDto dto, int sellerId)
        {
            var data = _mapper.Map<Product>(dto);
            data.SellerId = sellerId;
            var entity = await _repository.AddOneAsync(data);
            return _mapper.Map<Product, ProductReadDto>(entity);
        }
    }
}