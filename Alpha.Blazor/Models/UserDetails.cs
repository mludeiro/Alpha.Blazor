using System.Text.Json.Serialization;

namespace Alpha.Blazor.Models;

public class UserDetails
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("isEmailConfirmed")]
    public bool IsEmailConfirmed { get; set; }
}
