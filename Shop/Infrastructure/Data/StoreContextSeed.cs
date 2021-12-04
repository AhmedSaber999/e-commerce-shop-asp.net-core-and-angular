using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try{
                if(!context.ProductBrands.Any())
                {
                    var brands_data = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brands_data);
                    await context.ProductBrands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
                if(!context.ProductTypes.Any())
                {
                    var types_data = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(types_data);
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
                if(!context.Products.Any())
                {
                    var products_data = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var produts = JsonSerializer.Deserialize<List<Product>>(products_data);
                    foreach(var product in produts)
                        await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();
                }
            }catch(Exception ex){
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex, "An Error occured during seeding the data");
            }
        }
    }
}