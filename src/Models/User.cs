using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace Backend.src.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public Role Role { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    [PgName("role")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Buyer,
        Seller
    }
}