using System.ComponentModel.DataAnnotations;

namespace Alpha.Blazor.Models;

public record RegisterUser()
{
    [DataType(DataType.Text), Required]
    public string? User { get; set; }

    [DataType(DataType.Text), Required]
    public string? FirstName { get; set; }

    [DataType(DataType.Text), Required]
    public string? LastName { get; set; }


    [EmailAddress, Required]
    public string? Email { get; set; }
    
    [DataType(DataType.Password), Required]
    public string? Password { get; set; }
}
