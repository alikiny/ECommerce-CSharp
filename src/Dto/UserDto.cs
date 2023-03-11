namespace Backend.src.Dto
{
    public class UserDtoBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
    public class UserReadDto : UserDtoBase
    {
        public int ID { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class UserUpdateDto : UserDtoBase
    {
    }

    public class UserCreateDto : UserDtoBase
    {
        public string Password { get; set; } = null!;
        public Role Role { get; set; } = Role.Buyer;
    }

    public class UserAuthDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}