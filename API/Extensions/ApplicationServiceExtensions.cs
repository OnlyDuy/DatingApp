using API.Data;
using API.interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

    }
}