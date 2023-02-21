namespace Backend.src.Controllers
{
    public class OrderItemController : GenericController<OrderItem, OrderItemDto>
    {
        public OrderItemController(IBaseService<OrderItem, OrderItemDto> service) : base(service)
        {
        }
    }
}