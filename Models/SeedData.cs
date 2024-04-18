using BrazilBlend.Data;
using BrazilBlend.Models;
using Microsoft.EntityFrameworkCore;

namespace BrazilBlend.Models;

public static class SeedData {
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            if (context.Category.Any()) {
                return;
            }

            // Add Brand data.
            context.Brand.AddRange(
                new Brand {
                    Name = "NETCAFÉS",
                },
                new Brand {
                    Name = "Breville",
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