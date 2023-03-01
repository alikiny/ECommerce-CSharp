namespace Backend.src.Dto
{
    public class UserDtoBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class UserReadDto : UserDtoBase
    {
        public int ID { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class UserUpdateDto : UserDtoBase
    {
        public string Password { get; set; } = string.Empty;
    }

    public class UserCreateDto : UserDtoBase
    {
        public string Password { get; set; }
        public Role Role { get; set; } = Role.Buyer;
    }

    public class UserAuthDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}