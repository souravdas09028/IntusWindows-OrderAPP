using IntusWindows.Core.Data;
using IntusWindows.Service.Implementations;
using IntusWindows.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Api.Extention
{
    public static class AppServiceEnhancer
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<IntusWindowsDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IWindowRepository, WindowRepository>();
            //services.AddScoped<ISubElementRepository, SubElementRepository>();
            //services.AddScoped<IElementTypeRepository, ElementTypeRepository>();
        }
    }
}
