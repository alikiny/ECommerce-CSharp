namespace Backend.src.Services.OrderItemService
{
    public class OrderItemService : BaseService<OrderItem, OrderItemDto, OrderItemDto, OrderItemDto>, IOrderItemService
    {
        public OrderItemService(IMapper mapper, IOrderItemRepository repository) : base(mapper, repository)
        {
        }
    }

}