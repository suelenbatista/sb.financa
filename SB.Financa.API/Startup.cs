using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SB.Financa.DAL;
using SB.Financa.Generics.Helper;
using SB.Financa.Generics.Seguranca;
using SB.Financa.Model;

namespace SB.Financa.API
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
            services.AddControllers().AddNewtonsoftJson(
                options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            /* Setando o banco de dados */
            services.AddDbContext<FinancaContext>(options => {
                // options.UseSqlServer(Configuration.GetConnectionString("Financa"));
                options.UseSqlServer(SqlHelper.ObterConnectionString("FinancaDB"));
            });

            /* Injecao de dependencia para instaciar os repositorios   */
            services.AddTransient<IRepository<ContaBancaria>, RepositorioBaseEF<ContaBancaria>>();
            services.AddTransient<IRepository<ContaCartao>, RepositorioBaseEF<ContaCartao>>();
            services.AddTransient<IRepository<Etiqueta>, RepositorioBaseEF<Etiqueta>>();
            services.AddTransient<IRepository<Pessoa>, RepositorioBaseEF<Pessoa>>();
            services.AddTransient<IRepository<Planejamento>, RepositorioBaseEF<Planejamento>>();
            services.AddTransient<IRepository<Movimento>, RepositorioBaseEF<Movimento>>();
            services.AddTransient<IRepository<MovimentoBaixa>, RepositorioBaseEF<MovimentoBaixa>>();

            /* Repositorio especifico para o usuario*/
            services.AddTransient<RepositorioUsuarioEF<Usuario>, RepositorioUsuarioEF<Usuario>>();

            /* Identificacao da Secret (ChavePrivada) */
            byte[] chavePrivada = Encoding.ASCII.GetBytes(Settings.ChavePrivada);

            /* Authentificacao com JWT */
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>   // Informa as configuracoes de validacao do Token
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chavePrivada),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
