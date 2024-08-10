using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static partial class AppDependencyInjection
    {
        public static void  AddConfigurationApp(this IServiceCollection services)
        {
            services.AddScoped<ISale, SaleService>();
            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<ICost, CostService>();
            services.AddScoped<IUpload, UploadService>(); 
            services.AddScoped<IClient, ClientService>();
            services.AddScoped<IUser, UserService>();
        }
    }
}
