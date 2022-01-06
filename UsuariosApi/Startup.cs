using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UsuariosApi.Data;
using UsuariosApi.Services;

namespace UsuariosApi
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

            //Configura��o Banco de dados
            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("UsuarioConnection"));
            });


            //Configura��o Identity
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
                opt =>
                {
                    //aplicando configura��o para somenter logar quando o e-mail estiver confimado.
                    opt.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders(); //Linha para aplicar/gerar token, por exemplo: token de ativa��o para confirma��o de e-mail.

            services.AddScoped<CadastroService>();
            services.AddScoped<LoginService>();
            services.AddScoped<TokenService>();
            services.AddScoped<LogoutService>();
            services.AddScoped<EmailService>();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            /* services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "UsuariosApi", Version = "v1" });
             });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*
                 * app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuariosApi v1"));
                */
            }

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
