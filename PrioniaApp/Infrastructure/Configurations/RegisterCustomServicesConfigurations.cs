using PrioniaApp.Database;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Services.Abstracts;
using PrioniaApp.Services.Concretes;

namespace PrioniaApp.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();  
        }
    }
}
