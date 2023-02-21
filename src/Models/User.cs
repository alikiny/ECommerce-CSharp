using System.Diagnostics.CodeAnalysis;

namespace Backend.src.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}