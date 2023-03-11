namespace Backend.src.Dto
{
    public class ProductDtoBase
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public int CategoryID { get; set; }
        public int Inventory { get; set; }
    }

    public class ProductCreateDto : ProductDtoBase { }

    public class ProductReadDto : ProductDtoBase
    {
        public int ID { get; set; }
        public int SellerID { get; init; }
    }

    public class ProductUpdateDto : ProductDtoBase { }
}
