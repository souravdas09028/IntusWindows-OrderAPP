using IntusWindows.Web.Services.Implementations;
using IntusWindows.Web.Services.Interfaces;

namespace IntusWindows.Web.Extentions
{
    public static class AppServiceEnhancer
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWindowService, WindowService>();
            services.AddScoped<ISubElementService, SubElementService>();
        }
    }
}
