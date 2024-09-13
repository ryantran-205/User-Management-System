using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UMS.API.Models;

public class User : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    public DateTime DateCreated { get; set; } = DateTime.Now;
}