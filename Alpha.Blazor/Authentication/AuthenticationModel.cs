namespace Alpha.Blazor.Authentication;

public class AuthenticationModel
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

}
