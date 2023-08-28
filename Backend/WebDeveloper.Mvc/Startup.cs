using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDeveloper.Core.Interfaces;
using WebDeveloper.Core.Services;
using WebDeveloper.Infra.Data;

namespace WebDeveloper.Mvc
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
            // Configurar el esquema de autenticacion
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "CibertecAuth";
                    //config.LoginPath = "/Seguridad/Iniciar";
                })
                .AddGoogle(config =>
                {
                    config.ClientId = "212453236998-pj8nl0ve7rc6q88rntl2n39dcnv7ps81.apps.googleusercontent.com";
                    config.ClientSecret = "BRoCylFG5YwL7ej_rMUv4CeI";
                    config.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                    config.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
                    config.Events.OnTicketReceived = googleContext =>
                    {
                        //googleContext.HandleResponse();
                        var nameIdentifier = googleContext.Principal.FindFirstValue(ClaimTypes.NameIdentifier);

                        // Buscar el name identifier en la BD de usuarios
                        // Si existe, loguear (continuar)
                        // Si no existe, crear al nuevo usuario
                        // CreateUser(nameIdentifier)
                        return Task.CompletedTask;
                    };
                });
            // Configurar el servicio del ChinookContext (new ChinookContext("cadena"))
            services.AddDbContext<IChinookContext, ChinookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ChinookConnection")));

            // Inyectar la dependencia de los servicios, utilizando interfaces
            services.AddTransient<IReportesService, ReportesService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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

            app.UseAuthentication();
            // Esta linea tiene que ir siempre despues del routing y antes de los endpoints
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Demo}/{action=Index}/{id?}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "ruta-bienvenido",
            //        pattern: "Demo/Bienvenido/{nombre}");
            //});
        }
    }
}
