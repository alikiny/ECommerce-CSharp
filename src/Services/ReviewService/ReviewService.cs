using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Services.ReviewService
{
    public class ReviewService : BaseService<Category, CategoryDto>, ICategoryService
    {
        public ReviewService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}