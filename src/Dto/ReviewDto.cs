using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Dto
{
    public class ReviewDto: BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}