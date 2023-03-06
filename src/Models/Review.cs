using System.Text.Json.Serialization;
using NpgsqlTypes;

namespace Backend.src.Models
{
    public class Review : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Rating Rating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }

    [PgName("rating")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Rating
    {
        One = 1,
        Two,
        Three,
        Four,
        Five
    }
}