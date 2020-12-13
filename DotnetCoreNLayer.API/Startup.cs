using AutoMapper;
using DotnetCoreNLayer.API.DTO.Error;
using DotnetCoreNLayer.API.Extensions;
using DotnetCoreNLayer.API.Filters;
using DotnetCoreNLayer.Core.Repositories;
using DotnetCoreNLayer.Core.Services;
using DotnetCoreNLayer.Core.UnitOfWork;
using DotnetCoreNLayer.Data;
using DotnetCoreNLayer.Data.Repositories;
using DotnetCoreNLayer.Data.UnitOfWorks;
using DotnetCoreNLayer.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreNLayer.API
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
            // Converts models to DTO
            services.AddAutoMapper(typeof(Startup));
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DevelopmentConnection"].ToString(),
                    o => {
                        o.MigrationsAssembly("DotnetCoreNLayer.Data");
                    });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ProductNotFoundFilter>();
            services.AddScoped<CategoryNotFoundFilter>();

            services.AddControllers();
            // Validation filters can be used globally with the code below

            //services.AddControllers(options =>
            //{
            //    options.Filters.Add(new ValidationFilter());
            //}); 

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetCoreNLayer.API", Version = "v1" });
            });

            // ModelState invalid filters will be controlled manually.
            services.Configure<ApiBehaviorOptions>(options =>
            {                
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetCoreNLayer.API v1"));
            }

            // Used custom extion method
            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
