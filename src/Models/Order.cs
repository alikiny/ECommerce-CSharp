using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace Backend.src.Models
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public Paid Status { get; set; }
    }

    [PgName("paid")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Paid
    {
        True,
        False,
        Pending
    }
}