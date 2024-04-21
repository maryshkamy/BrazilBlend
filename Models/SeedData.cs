using BrazilBlend.Data;
using BrazilBlend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrazilBlend.Models;

public static class SeedData {
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync("Admin")) {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (!await roleManager.RoleExistsAsync("User")) {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var admin = new IdentityUser
        {
            UserName = "admin@brazilblend.com",
            Email = "admin@brazilblend.com",
            EmailConfirmed = true
        };

        var adminExists = await userManager.FindByEmailAsync(admin.Email);

        if (adminExists is null)
        {
            await userManager.CreateAsync(admin, "Brazil@123");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        var user = new IdentityUser
        {
            UserName = "mariana@brazilblend.com",
            Email = "mariana@brazilblend.com",
            EmailConfirmed = true
        };

        var userExists = await userManager.FindByEmailAsync(user.Email);

        if (userExists is null)
        {
            await userManager.CreateAsync(user, "Brazil@123");
            await userManager.AddToRoleAsync(user, "User");
        }

        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Category.Any()) {
                return;
            }

            // Add Brand data.
            context.Brand.AddRange(
                new Brand {
                    Name = "Breville",
                },
                new Brand {
                    Name = "Fellow",
                },
                new Brand {
                    Name = "Intelligenza"
                },
                new Brand {
                    Name = "Keurig"
                },
                new Brand {
                    Name = "NETCAFÉS",
                },
                new Brand {
                    Name = "Starbucks"
                }
            );

            // Add Category data.
            context.Category.AddRange(
                new Category {
                    Name = "Whole Bean Coffee"
                },
                new Category {
                    Name = "Keurig K-Cups",
                },
                new Category {
                    Name = "Equipment",
                },
                new Category {
                    Name = "Accessories",
                }
            );

            context.SaveChanges();
        }
    }
}