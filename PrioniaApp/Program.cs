using Microsoft.EntityFrameworkCore;
using PrioniaApp.Infrastructure.Extensions;
using System;

namespace PrioniaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup
            var builder = WebApplication.CreateBuilder(args);

            //Register services (IoC container)
            builder.Services.ConfigureServices(builder.Configuration);

            //setup
            var app = builder.Build();

            //Configuration of middleware pipeline
            app.ConfigureMiddlewarePipeline();

            //setup
            app.Run();
        }
    }
}