using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TheBookWebApi.Controllers.ActionFilters;
using TheBookWebApi.Models.DB;

namespace TheBookWebApi
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
            services.AddDbContext<DefaultContext>(opt => opt.UseSqlServer(Configuration["Data:TheBook:ConnectionString"]));

            services.Configure<ApiBehaviorOptions>(option =>
            {
                option.SuppressConsumesConstraintForFormFileParameters = true;
                option.SuppressInferBindingSourcesForParameters = true;
                option.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default 
                options.Filters.Add(typeof(ApiValidationFilterAttribute));
                options.Filters.Add(new ApiExceptionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddJsonOptions(opts =>
        {
            opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            opts.SerializerSettings.NullValueHandling = NullValueHandling.Include;
        });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddJsonOptions(opt =>
            //    {
            //        opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            //        opt.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            //    });
      
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseDefaultFiles();
            app.UseStaticFiles();

          //  app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
