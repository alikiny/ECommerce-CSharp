namespace Backend.src.Services.CategoryService
{
    public class CategoryService : BaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryService
    {
        public CategoryService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}