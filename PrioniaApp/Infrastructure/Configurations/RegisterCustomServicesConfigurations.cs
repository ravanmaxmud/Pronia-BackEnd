using PrioniaApp.Database;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Services.Abstracts;
using PrioniaApp.Services.Concretes;
using PrioniaApp.Areas.Client.ActionFilter;

namespace PrioniaApp.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<ValidationCurrentUserAttribute>();
        }
    }
}
