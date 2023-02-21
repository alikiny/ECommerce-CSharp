using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Controllers
{
    public class CategoryController : GenericController<Category, CategoryDto>
    {
        public CategoryController(IBaseService<Category, CategoryDto> service) : base(service)
        {
        }
    }
}