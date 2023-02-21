using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Models
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Paid Status { get; set; }
    }
}