using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projeto.Interfaces;
using Projeto.Mock;
using Projeto.Services;

namespace Projeto
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
            // add authentication scheme
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.AccessDeniedPath = "/Interno/AccessDenied";
                });

            services.AddHttpContextAccessor(); // <-- acessar session fora do controller (injeção)                       
            
            services.AddControllersWithViews();

            // injeção de outros serviços
            services.AddSingleton<GetUserAuth>();
            services.AddSingleton<IUserService, UserServiceFake>();
            services.AddSingleton<IBancoService, BancoServiceFake>();
            services.AddSingleton<IBancoPercentualService, BancoPercentualServiceFake>();
            services.AddSingleton<IClienteService, ClienteServiceFake>();
            services.AddSingleton<IDadosClienteService, DadosClienteServiceFake>();

            services.AddSingleton<UserRepositoryFake>();
            services.AddSingleton<BancoRepositoryFake>();
            services.AddSingleton<BancoPercentualRepositoryFake>();
            services.AddSingleton<ClienteRepositoryFake>();
            services.AddSingleton<DadosClienteRepositoryFake>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

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
