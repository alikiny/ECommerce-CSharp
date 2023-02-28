namespace Backend.src.Dto
{

    public class UserDtoBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
    public class UserReadDto : UserDtoBase
    {
        public Role Role { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class UserUpdateDto : UserDtoBase
    {
        public string? PasswordRaw { get; set; }
    }

    public class UserCreateDto : UserDtoBase
    {
        public string PasswordRaw { get; set; }
    }

    public class UserAuthDto : UserDtoBase
    {
        public string PasswordRaw { get; set; }
    }
}