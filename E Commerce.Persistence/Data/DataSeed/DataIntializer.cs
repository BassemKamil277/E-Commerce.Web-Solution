using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites;
using E_Commerce.Domain.Entites.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeed
{
    public class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext )
        {
           _dbContext = dbContext;
        }
        public void Intialize()
        {
            try
            {
                var HasProducts = _dbContext.Products.Any();
                var HasBrands = _dbContext.ProductBrands.Any();
                var HasType = _dbContext.ProductTypes.Any();

                if (HasProducts && HasBrands && HasType) return;

                if (!HasBrands)
                {
                    SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }
                if (!HasType)
                {
                    SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);

                }
                _dbContext.SaveChanges(); // lazm a3ml add abl lma a7ot data fy el Product 34an el schema 

                if (!HasBrands)
                {
                    SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                }
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Data seed is failed : {ex}");
            }
        }

        private void SeedDataFromJson<T , TKey>(string FileName , DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            // C:\Users\Bassem\Downloads\Route\APIs\E Commerce.Web Solution\E Commerce.Persistence\Data\DataSeed\JSONFiles\brands.json

            var FilePath = @"..\E Commerce.Persistence\Data\DataSeed\JSONFiles\" + FileName;

            if (!File.Exists(FilePath)) throw new FileNotFoundException ($" file {FileName} is not exist");

            try
            {
                // var Data = File.ReadAllText(FilePath); tnf3 lw el data msh kbera 

                using var dataStreams = File.OpenRead(FilePath);

                var data = JsonSerializer.Deserialize<List<T>>(dataStreams, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true

                });
                if(data is not null)
                {
                    dbset.AddRange(data);
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error whie reeding json file : {ex}");
                return;
            }
            
        }
    }
}
