using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minimarket.Infrastructure.Data;
using Minimarket.Infrastructure.Repositories;

namespace Minimarket.Infrastructure.Extensions
{
    /// <summary>
    /// Extensiones para la configuración de servicios de Infrastructure
    /// </summary>
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Configurar DbContext con SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Registrar Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Registrar repositorios genéricos
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}