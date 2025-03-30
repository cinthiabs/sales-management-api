using Infrastructure.Cache;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DI
{
    public static partial class DataDependencyInjection
    {
        public static void AddConfigurationData(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddCache();
        }

        public static void AddCache(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddStackExchangeRedisCache(o => {
                o.InstanceName = "instance";
                o.Configuration = "localhost:6379";
            });
        }

        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IZipCodeRepository, ZipCodeRepository>();
            services.AddScoped<IProductCostRepository, ProductCostRepository>();
        }
    }
}
