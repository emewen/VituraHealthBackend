using System.Text.Json.Serialization;

namespace VituraHealthBackend.Models
{
    public class Patient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fullName")]
        public required string FullName { get; set; }
        [JsonPropertyName("dateOfBirth")]
        public DateOnly DateOfBirth { get; set; }
    }
}
