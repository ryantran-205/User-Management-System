using System.ComponentModel.DataAnnotations;

namespace UMS.API.DTOs.Account;

public class LoginDto
{
    [Required]
    public string UserName { get; set; } = default!;
    
    [Required]
    public string Password { get; set; } = default!;
}