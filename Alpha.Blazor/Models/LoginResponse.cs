using System.Text.Json.Serialization;

namespace Alpha.Blazor.Models;

public class LoginResponse
{

    [JsonPropertyName("token")]
    public string? Token { get; set; }
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
}
