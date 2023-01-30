using Microsoft.Extensions.Logging;
using Route.DAL.Entities;
using Route.DAL.Entities.Order;
using System.Text.Json;

namespace Route.DAL.Data
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
		{
			try
			{
				if (!context.ProductBrands.Any())
				{
					var brandData = File.ReadAllText("../Route.DAL/Data/SeedData/brands.json");
					var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
					foreach (var brand in brands)
					{
						context.ProductBrands.Add(brand);
					}
					await context.SaveChangesAsync();
				}

				if (!context.ProductTypes.Any())
				{
					var typeData = File.ReadAllText("../Route.DAL/Data/SeedData/types.json");
					var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
					foreach (var type in types)
					{
						context.ProductTypes.Add(type);
					}
					await context.SaveChangesAsync();
				}

				if (!context.Products.Any())
				{
					var productData = File.ReadAllText("../Route.DAL/Data/SeedData/products.json");
					var products = JsonSerializer.Deserialize<List<Product>>(productData);
					foreach (var product in products)
					{
						context.Products.Add(product);
					}
					await context.SaveChangesAsync();
				}

				if (!context.DelieveryMethods.Any())
				{
					var delieveryMethod = File.ReadAllText("../Route.DAL/Data/SeedData/delivery.json");
					var methods = JsonSerializer.Deserialize<List<DelieveryMethod>>(delieveryMethod);
					foreach (var method in methods)
					{
						context.DelieveryMethods.Add(method);
					}
					await context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{

				var logger = loggerFactory.CreateLogger<StoreContextSeed>();
				logger.LogError(ex.Message);
			}
		}
	}
}
