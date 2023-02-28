namespace Backend.src.Services.OrderService
{
    public class OrderService : BaseService<Order, OrderDto, OrderDto, OrderDto>, IOrderService
    {
        public OrderService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}