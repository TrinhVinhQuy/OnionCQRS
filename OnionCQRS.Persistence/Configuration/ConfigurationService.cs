using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Persistence.Data;
using OnionCQRS.Persistence.Repository;

namespace OnionCQRS.Persistence.Configuration
{
    public static class ConfigurationService
    {
        public static async Task AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await appContext.Database.MigrateAsync();
            }
        }
        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Add AddScoped: Sử dụng để chia sẻ cùng một instance trong suốt một yêu cầu HTTP
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
