using fp_18_web_aula_1_core.Data;
using fp_18_web_aula_1_core.Identity;
using fp_18_web_aula_1_core.Services;
using fp_web_aula_1.Controllers;
using fp_web_aula_1_core.Data;
using fp_web_aula_1_core.Models;
using fp_web_aula_1_core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Text;

namespace fp_web_aula_1
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IChaveService, ChaveService>();
            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddTransient<ILogerApi, LogerApi>();
            services.AddTransient<Authentication>();
            services.AddTransient<Manager>();
            services.AddTransient<RoleManager<IdentityRole>>();


            services.AddTransient<IdentityRole>();

            services.AddDbContext<CopaContext>
                (options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddMemoryCache();

            services.AddMvc();

            services.Configure<GzipCompressionProviderOptions>(
              o => o.Level = System.IO.Compression.CompressionLevel.Fastest);

            services.AddResponseCompression(o =>
            {
                o.Providers.Add<GzipCompressionProvider>();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 3;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<CopaContext>()
          .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Index";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, RoleManager<IdentityRole> _roleManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ApplicationDbInitializer.SeedRoles(_roleManager);
            }

            app.UseMeuMiddleware();

            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

            app.UseAuthentication();

            app.UseMvc(r =>
            {
                r.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
