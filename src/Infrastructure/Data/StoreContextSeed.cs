using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastructure.Data
{
  public class StoreContextSeed
  {
    public static async Task SeedAsync(StoreContext context)
    {
      if (!context.ProductBrands.Any())
      {
        var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
        var brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);
        context.ProductBrands.AddRange(brands);
      }

      if (!context.ProductTypes.Any())
      {
        var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
        var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);
        context.ProductTypes.AddRange(types);
      }

      if (!context.Products.Any())
      {
        var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
        var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productsData);
        context.Products.AddRange(products);
      }

      if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
  }
}