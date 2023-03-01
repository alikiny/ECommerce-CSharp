namespace Backend.src.Dto
{
    public class OrderDto: BaseModel
    {
        public int UserId { get; set; }
        public Paid Status { get; set; }
    }
}