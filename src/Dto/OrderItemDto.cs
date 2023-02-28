using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Dto
{
    public class OrderItemDto: BaseModel
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}