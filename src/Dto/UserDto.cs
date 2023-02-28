namespace Backend.src.Dto
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordRaw { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class UserAuthDto
    {
        public string Email { get; set; }
        public string PasswordRaw { get; set; }
    }
}