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