namespace Backend.src.Dto
{
    public class ProductDto
    {
        public string Title { get; set; }
        public long Description { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public int SellerID { get; init; }
        public int Inventory { get; set; }
    }
}