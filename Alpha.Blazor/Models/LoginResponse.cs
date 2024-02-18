using System.Text.Json.Serialization;

namespace Alpha.Blazor.Models;

public class LoginResponse
{
    [JsonPropertyName("tokenType")]
    public string? TokenType { get; set; }
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; set; }
}
