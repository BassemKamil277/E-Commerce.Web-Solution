
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeed;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.Repositories;
using E_Commerce.Services;
using E_Commerce.Services.MappingProfile;
using E_Commerce.Services_Abstraction;
using E_Commerce.Web.Extentions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataIntializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new ProductProfile()));
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReferences).Assembly);
            //builder.Services.AddAutoMapper(x => x.LicenseKey = "", typeof(ProductProfile).Assembly); // kda h7l moshklt el license w kman msh kol m a3ml mapping l section aro7 a3mlo allow , bs el klam da fy akher version ll package

            builder.Services.AddScoped<IProductService , ProductService>();
            builder.Services.AddTransient<ProductPictureUrlResolver>();
            #endregion

            var app = builder.Build();

            #region DataSeed - Apply Migration;
           await app.MigrateDatabaseAsync();
           await app.SeedDatabaseAsync();

            #endregion

            #region Configure the HTTP request pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(); //34an te2ra el photos ,  fe verion  bs lkn fe el last version msh b7tag aktbha 
            app.UseHttpsRedirection();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
