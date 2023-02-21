using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Controllers
{
    public class OrderController : GenericController<Order, OrderDto>
    {
        public OrderController(IBaseService<Order, OrderDto> service) : base(service)
        {
        }
    }
}