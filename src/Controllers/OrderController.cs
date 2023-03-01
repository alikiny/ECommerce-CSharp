namespace Backend.src.Controllers
{
    public class OrderController : GenericController<Order, OrderDto, OrderDto, OrderDto>
    {
        public OrderController(IOrderService service) : base(service)
        {
        }
    }
}