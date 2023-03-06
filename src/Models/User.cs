using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace Backend.src.Models
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
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