using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int SellerId { get; set; }
        public virtual User User { get; init; }
        public int Inventory { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}