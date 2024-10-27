using Infrastructure.DI;
using Application.DI;

namespace Api.DI
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPresentationDI(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddConfigurationData();
            services.AddConfigurationApp();
            return services;
        }
    }
}
