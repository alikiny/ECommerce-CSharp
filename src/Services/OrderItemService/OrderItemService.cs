namespace Backend.src.Services.OrderItemService
{
    public class OrderItemService : BaseService<OrderItem, OrderItemDto>, IOrderItemService
    {
        public OrderItemService(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }

}