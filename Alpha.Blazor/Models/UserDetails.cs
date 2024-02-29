using System.Text.Json.Serialization;

namespace Alpha.Blazor.Models;

public class UserDetails
{
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("isEmailConfirmed")]
    public bool IsEmailConfirmed { get; set; }
}
