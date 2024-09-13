using System.ComponentModel.DataAnnotations;

namespace UMS.API.DTOs.Account;

public class RegisterDto
{
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "FirstName must be between 3 and 15 characters")]
    public string FirstName { get; set; } = default!;
    
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "LastName must be between 3 and 15 characters")]
    public string LastName { get; set; } = default!;
    
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = default!;
    
    [Required]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 15 characters")]
    public string Password { get; set; } = default !;
}