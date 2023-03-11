namespace Backend.src.Dto
{
    public class CategoryDtoBase
    {
        public string Name { get; set; } = null!;
    }

    public class CategoryCreateDto : CategoryDtoBase
    {

    }

    public class CategoryReadDto : CategoryDtoBase
    {
        public int ID { get; set; }
    }

    public class CategoryUpdateDto : CategoryDtoBase
    {

    }
}