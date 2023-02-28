using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Controllers
{
    public class OrderController : GenericController<Order, OrderDto, OrderDto, OrderDto>
    {
        public OrderController(IOrderService service) : base(service)
        {
        }
    }
}