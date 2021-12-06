using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ParkyAPI.AutoMapper;
using ParkyAPI.Data;
using ParkyAPI.Repository.Implement;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParkyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DATABASE CONNECTION SETUP

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // REGISTER NATIONAL PARK REPOSITORY
            services.AddScoped<INationalParkRepository, NationalParkRepository>();
            services.AddScoped<ITrailRepository, TrailRepository>();

            // AUTO MAPPER
            services.AddAutoMapper(typeof(AutoMapperConfiguration));



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("NationalPark", new OpenApiInfo { 
                    Title = "National Park", 
                    Version = "v1",

                    Description = "National Park Controller",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "lindosmahadi@gmail.com",
                        Name = "lindos mahadi",
                        Url = new Uri("https://wwww.github.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }

                });

                c.SwaggerDoc("Trail", new OpenApiInfo
                {
                    Title = "Trail",
                    Version = "v1",

                    Description = "Trail Controller",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Email = "lindosmahadi@gmail.com",
                        Name = "lindos mahadi",
                        Url = new Uri("https://wwww.github.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")
                    }

                });

                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                c.IncludeXmlComments(cmlCommentsFullPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/NationalPark/swagger.json", "National Park API");
                    c.SwaggerEndpoint("/swagger/Trail/swagger.json", "Trail API");
                    c.RoutePrefix = "";
                    });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
