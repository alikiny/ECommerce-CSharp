namespace Backend.src.Dto
{
    public class ReviewDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Rating Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}