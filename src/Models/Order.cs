namespace Backend.src.Models
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Paid Status { get; set; }
    }
}