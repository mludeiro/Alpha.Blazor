using System.ComponentModel.DataAnnotations;

namespace Alpha.Blazor.Models;

public record LoginUser()
{
    [DataType(DataType.Text), Required]
    public string? User { get; set; }
    [DataType(DataType.Password), Required]
    public string? Password { get; set; }
}
