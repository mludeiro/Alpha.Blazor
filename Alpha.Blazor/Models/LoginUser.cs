using System.ComponentModel.DataAnnotations;

namespace Alpha.Blazor.Models;

public record LoginUser()
{
    [EmailAddress, Required]
    public string? Email { get; set; }
    [DataType(DataType.Password), Required]
    public string? Password { get; set; }
}
