namespace Backend.src.Services.CategoryService
{
    public class CategoryService : BaseService<Category, CategoryDto, CategoryDto, CategoryDto>, ICategoryService
    {
        public CategoryService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}