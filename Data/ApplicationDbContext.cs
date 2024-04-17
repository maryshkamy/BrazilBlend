using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrazilBlend.Models;

namespace BrazilBlend.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<BrazilBlend.Models.Stock> Stock { get; set; } = default!;

public DbSet<BrazilBlend.Models.Product> Product { get; set; } = default!;

public DbSet<BrazilBlend.Models.Category> Category { get; set; } = default!;
}
