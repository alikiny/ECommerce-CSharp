using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; } = null!;
        public int SellerId { get; set; }
        public virtual User User { get; init; } = null!;
        public int Inventory { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}