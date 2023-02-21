using System.Text.Json.Serialization;

namespace Backend.src.Models
{
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