using Giny.Auth;
using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.IO.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Web
{
    internal class WebApi
    {
        [StartupInvoke("WebApi", StartupInvokePriority.Last)]
        public static void Start()
        {
            var builder = WebApplication.CreateBuilder();


            builder.Logging.ClearProviders();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var config = ConfigManager<AuthConfig>.Instance;


            string apiUrl = $"http://{config.APIHost}:{config.APIPort}";

            app.Urls.Clear();


            app.Urls.Add(apiUrl);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();

            Thread thread = new Thread(app.Run);
            thread.Start();

            Logger.Write($"Web api started '{apiUrl}'");

        }
    }
}
