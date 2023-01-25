using PrioniaApp.Database;
using PrioniaApp.Options;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Infrastructure.Configurations;
using System.Text.Json.Serialization;
using PrioniaApp.Areas.Client.ActionFilter;
using AspNetCore.IServiceCollection.AddIUrlHelper;

namespace PrioniaApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o =>
            {
                o.Cookie.Name = "Identity";
                o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                o.LoginPath = "/authentication/login";
 
            });

            services.AddHttpContextAccessor();

            services.AddUrlHelper();

            services.AddScoped<ValidationCurrentUserAttribute>();

            services.ConfigureMvc();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
