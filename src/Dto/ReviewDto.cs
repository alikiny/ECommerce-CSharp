namespace Backend.src.Dto
{
    public class ReviewDto: BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}