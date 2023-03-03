using System.Text.Json.Serialization;

namespace Backend.src.Models
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public Paid Status { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Paid
    {
        True,
        False,
        Pending
    }
}