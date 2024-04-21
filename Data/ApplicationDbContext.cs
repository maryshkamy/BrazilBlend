using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BrazilBlend.Models;

namespace BrazilBlend.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Brand> Brand { get; set; } = default!;
    public DbSet<CartItem> CartItem { get; set; } = default!;
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<Order> Order { get; set; } = default!;
    public DbSet<OrderItem> OrderItem { get; set; } = default!;
    public DbSet<OrderStatus> OrderStatus { get; set; } = default!;
    public DbSet<Product> Product { get; set; } = default!;
    public DbSet<ShoppingCart> ShoppingCart { get; set; } = default!;
}