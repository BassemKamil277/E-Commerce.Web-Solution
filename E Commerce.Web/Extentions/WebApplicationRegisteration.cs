using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extentions
{
    public static class WebApplicationRegisteration
    {
        public static async Task<WebApplication> DatabaseMigrateAsync(this WebApplication app)
        {
            await using var Scope =  app.Services.CreateAsyncScope();
            var DbContextService = Scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var PendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
               await DbContextService.Database.MigrateAsync();
            return app;
        }
        public static async Task<WebApplication> DatabaseIdentityMigrateAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DbContextService = Scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var PendingMigrations = await DbContextService.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                await DbContextService.Database.MigrateAsync();
            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DbContextService = Scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var DataIntializerService = Scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Default");
            await DataIntializerService.IntializeAsync();
            return app;
        }

        public static async Task<WebApplication> SeedIdentityDatabaseAsync(this WebApplication app)
        {
            await using var Scope = app.Services.CreateAsyncScope();
            var DbContextService = Scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
            var DataIntializerService = Scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Identity");
            await DataIntializerService.IntializeAsync();
            return app;
        }
    }
}
