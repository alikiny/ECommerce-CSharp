namespace Backend.src.Services.ProductService
{
    public class ProductService : BaseService<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>, IProductService
    {
        public ProductService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public async Task<ProductReadDto> AddOneAsync(ProductCreateDto dto, int sellerId)
        {
            var entity = _mapper.Map<Product>(dto);
            entity.SellerId = sellerId;
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<Product, ProductReadDto>(entity);
        }
    }
}