using FinalProject.Hubs;
using FinalProject.Services.Plane_service;
using FinalProject.Services.Repositries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<PlaneContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
            services.AddSingleton<IRepository, PlaneRep>();
            services.AddSingleton<IAirport, Airport>();
          
            services.AddCors(option =>
           option.AddDefaultPolicy(builder =>
           {
               builder.WithOrigins("http://localhost:3000").
               AllowAnyHeader().
               AllowAnyMethod().
               AllowCredentials();
           }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PlaneContext cntx)
        {
            cntx.Database.EnsureDeleted();
            cntx.Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else if (env.IsStaging() || env.IsProduction())
            {
                app.UseExceptionHandler("/Error/Index");
            }
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MainHub>("/MainHub");
            });
        }
    }
}
