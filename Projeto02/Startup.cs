using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Projeto02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
           
        public IConfiguration Configuration { get; }
        //****************************************************************
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            
                //Configurando a injeção de dependência
            services.AddScoped<MedicoDao>();
            services.AddScoped<PacienteDao>();
            services.AddScoped<CategoriaDao>();
            services.AddScoped<ConsultaDao>();
            services.AddScoped<DisponibilidadeDao>();
            services.AddHttpContextAccessor();

            services.AddDbContext<Context>
                (options => options.UseSqlServer
                (Configuration.GetConnectionString
                ("ProjetoWeb")));

            //Configuração da sessão deve ser colocada ANTES
            //do services.AddMvc()
            services.AddSession();
            services.AddDistributedMemoryCache();

            //Configurar o Identity na aplicação
            services.AddIdentity<UsuarioLogado, IdentityRole>().
                AddEntityFrameworkStores<Context>().
                AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Paciente/Login";
                options.LoginPath = "/Medico/Login";
                options.AccessDeniedPath = "/Paciente/AcessoNegado";
                options.AccessDeniedPath = "/Medico/AcessoNegado";
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Cria sozinho as migrações do banco 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
            }

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
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
