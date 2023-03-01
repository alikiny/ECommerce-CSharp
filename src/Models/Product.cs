using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.src.Models
{
    public class Product : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int SellerId { get; init; }
        public User User { get; init; }
        public int Inventory { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}