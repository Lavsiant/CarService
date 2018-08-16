using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Services.Implementation;
using CarService.Services.Interfaces;
using DBRepository;
using DBRepository.Factories;
using DBRepository.Interfaces;
using DBRepository.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
//using DBRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace CarService
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddScoped<IRepositoryContextFactory, RepositoryContextFactory>();
            services.AddScoped<IIdentityRepository>(provider => new IdentityRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ICarRepository>(provider => new CarRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<IOwnerRepository>(provider => new OwnerRepository(Configuration.GetConnectionString("DefaultConnection"), provider.GetService<IRepositoryContextFactory>()));
            services.AddScoped<ICarService, CarService.Services.Implementation.CarService>();
            services.AddScoped<IAccountService, CarService.Services.Implementation.AccountService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Cars}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "sarch_car_route",
                    template: "{controller=Cars}/{action=Index}/{model?}/{series?}/{carType?}");
            });
        }
    }
}
