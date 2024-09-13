using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMS.API.Models;

namespace UMS.API.Data;

public class Context(DbContextOptions<Context> options) : IdentityDbContext<User>(options);