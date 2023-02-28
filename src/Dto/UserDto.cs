namespace Backend.src.Dto
{

    public class UserDtoBase: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    public class UserReadDto : UserDtoBase
    {
        public Role Role { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class UserUpdateDto : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PasswordRaw { get; set; }
    }

    public class UserCreateDto : UserDtoBase
    {
        public string PasswordRaw { get; set; }
        public Role Role { get; set; } = Role.Buyer;
    }

    public class UserAuthDto
    {
        public string Email { get; set; }
        public string PasswordRaw { get; set; }
    }
}