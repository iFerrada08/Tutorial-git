using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using WebDeveloper.MvcIdentity.Model;

namespace WebDeveloper.MvcIdentity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar el DbContext que utilizara ASP net Identity
            services.AddDbContext<ChinookIdentityContext>(options =>
            {
                options.UseSqlServer("server=.;database=chinook_identity;trusted_connection=true;");
            });

            // Configurar el Identity
            services.AddIdentity<ChinookUser, IdentityRole>(config=>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                // Con este flag hacemos que los usuarios que inicien sesion tengan que tener su cuenta activada
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ChinookIdentityContext>()
                .AddDefaultTokenProviders();

            // Configurar la autenticacion
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Identity.Chinook";
                config.LoginPath = "/Home/Login";
            });


            // configurar mailkit
            services.AddMailKit(optionBuilder =>
            {
                // Para produccion, esto debe venir de un arhcivo de configuracion (json)
                optionBuilder.UseMailKit(new MailKitOptions()
                {
                    Server = "127.0.0.1",
                    Port = 25,
                    SenderName = "Soporte",
                    SenderEmail = "soporte@chinook.com",
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
