using WebAPI.Data.Contexts;
using WebAPI.Data.Entities;

namespace WebAPI.Data.SeedData
{
    public static class SeedData
    {
        public static async Task SeedProducts(DataContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new ProductEntity { Name = "Ethiopian Yirgacheffe", Description = "Floral aroma with citrus notes and a smooth body.", Price = 14.99m },
                new ProductEntity { Name = "Colombian Supremo", Description = "Balanced flavor with a nutty undertone.", Price = 13.49m },
                new ProductEntity { Name = "Guatemalan Antigua", Description = "Rich and full-bodied with hints of chocolate and spice.", Price = 15.25m },
                new ProductEntity { Name = "Sumatra Mandheling", Description = "Earthy and complex with low acidity.", Price = 14.75m },
                new ProductEntity { Name = "Kenya AA", Description = "Bright acidity and fruity flavor, with a wine-like finish.", Price = 16.00m },
                new ProductEntity { Name = "Brazil Santos", Description = "Mild, smooth flavor with a hint of chocolate.", Price = 12.95m },
                new ProductEntity { Name = "Costa Rican Tarrazú", Description = "Clean, crisp taste with lively acidity.", Price = 13.99m },
                new ProductEntity { Name = "Jamaican Blue Mountain", Description = "Smooth, well-balanced, and mild flavor.", Price = 35.00m },
                new ProductEntity { Name = "Hawaiian Kona", Description = "Mellow and aromatic with medium body.", Price = 28.00m },
                new ProductEntity { Name = "Panama Geisha", Description = "Highly floral and tea-like with jasmine and bergamot notes.", Price = 40.00m }
                );

                await context.SaveChangesAsync();
            }

            return;
        }
    }
}
