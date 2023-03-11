using System.Diagnostics;
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
                .ToArray();
            var dtoTypes = typeof(Program).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Dto"))
                .ToArray();
            foreach (var modelType in modelTypes)
            {
                var matchTypes = dtoTypes.Where(t => 
                t.Name.StartsWith(modelType.Name) &
                t.Name.EndsWith("Dto") &
                t.Name != "UserCreateDto");
                foreach (var matchType in matchTypes)
                {
                    CreateMap(matchType, modelType);
                    CreateMap(modelType, matchType);
                }
            }

            //ignore mapping property Password from UserCreateDto to UserDto because of incompatible type
            CreateMap<UserCreateDto, User>().ForMember(user => user.Password, opt => opt.Ignore());
        }
    }
}