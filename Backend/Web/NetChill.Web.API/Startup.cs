using AutoMapper;
using NetChill.Project.Loggig.NLog;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetChil.Project.Foundation.Core.ExceptionManagement;
using NetChill.Project.DataAccess.Data.Dbcontext;
using NetChill.Project.Foundation.Core.Logging;
using NetChill.Project.UserDomains.AppServices;
using NetChill.Project.UserDomains.Configuration;
using System;
using NetChill.Project.UserDomains.AppServices.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NetChill.Project.MovieDomains.Configuration;
using NetChill.Project.MovieDomains.AppServices.Mapper;
using NetChill.Project.MovieDomains.AppServices;
using System.Data.SqlClient;

namespace NetChill.Web.API
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new WebMappingProfile());
                cfg.AddProfile(new MappingProfile());
                cfg.AddProfile(new MappingProfile1());
            });

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddCors();
            services.RegisterRepositories();
            services.RegisterRepositories1();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "localhost",
                        ValidAudience = "localhost",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtConfig:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSingleton<IMapper>(sp => _mapperConfiguration.CreateMapper());
            services.AddSingleton<IMovieAppService, MovieAppService>();
            services.AddSingleton<IUserAppService, UserAppService>();
            services.AddSingleton<IExceptionManager, ExceptionManager>();
            services.AddSingleton<ILogger, Logger>();
            services.AddDbContext<NetChillDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocConnectionString")), ServiceLifetime.Singleton);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(m => m.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
