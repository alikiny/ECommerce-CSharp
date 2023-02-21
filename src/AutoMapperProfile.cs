using System.Reflection;

namespace Backend.src
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /* CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>(); */

            /* dynamically add all the models and dtos */
            var modelTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => typeof(BaseModel).IsAssignableFrom(t))
                .ToList();
            var dtoTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Dto"))
                .ToList();
            foreach (var modelType in modelTypes)
            {
                var dtoType = dtoTypes.FirstOrDefault(t => t.Name == modelType.Name + "Dto");

                if (dtoType != null)
                {
                    CreateMap(modelType, dtoType);
                    CreateMap(dtoType, modelType);
                }
            }
        }
    }
}