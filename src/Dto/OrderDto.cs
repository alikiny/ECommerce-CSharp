using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Dto
{
    public class OrderDto: BaseModel
    {
        public int UserId { get; set; }
        public Paid Status { get; set; }
    }
}