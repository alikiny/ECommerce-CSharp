namespace Backend.src.Dto
{
    public class ProductDtoBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public int SellerID { get; init; }
        public int Inventory { get; set; }
    }

    public class ProductCreateDto: ProductDtoBase
    {

    }

    public class ProductReadDto: ProductDtoBase
    {
        public int ID { get; set; }
    }

    public class ProductUpdateDto: ProductDtoBase
    {

    }
}