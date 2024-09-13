namespace UMS.API.DTOs.Account;

public class UserDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Jwt { get; set; } = default!;
}