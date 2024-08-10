using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public static partial class DataDependencyInjection
    {
        public static void AddConfigurationData(this IServiceCollection services)
        {
            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
