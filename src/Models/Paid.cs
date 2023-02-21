using System.Text.Json.Serialization;

namespace Backend.src.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Paid
    {
        True,
        False,
        Pending
    }
}