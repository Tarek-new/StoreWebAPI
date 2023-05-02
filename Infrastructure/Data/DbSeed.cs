using Core.Enitities;
using Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Infrastructure.Seeds
{
    public class DbContextSeed
    {
        public static async Task SeedDataAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {

                    var brandData = File.ReadAllText("../Infrastructure/Data/Seeds/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);

                    }
                    await context.SaveChangesAsync();
                }

                if (context.ProductTypes != null && !context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/Seeds/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var type in types)
                    {
                        await context.ProductTypes.AddAsync(type);

                    }
                    await context.SaveChangesAsync();
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/Seeds/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var product in products)
                    
                         context.Products.Add(product);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<DbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
