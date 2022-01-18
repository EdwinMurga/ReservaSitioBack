using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservaSitio.Abstraction;
using ReservaSitio.Application;
using ReservaSitio.DataAccess;
using ReservaSitio.Repository;
using ReservaSitio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservaSitio.Application.Autenticacion;

using ReservaSitio.DataAccess.CustomConnection;
using ReservaSitio.Abstraction.IRepository;

using ReservaSitio.Abstraction.IApplication.Auth;
using ReservaSitio.Abstraction.IRepository.Auth;
using ReservaSitio.Abstraction.IService.Auth;

using ReservaSitio.Application.Auth;
using ReservaSitio.Repository.Auth;
using ReservaSitio.Services.Auth;


using ReservaSitio.Services.AzureBlodStorage;
using ReservaSitio.Abstraction.IService.AzureBlobStorage;
using ReservaSitio.Abstraction.IApplication.Opciones;
using ReservaSitio.Abstraction.IService.Opciones;
using ReservaSitio.Abstraction.IRepository.Opciones;
using ReservaSitio.Application.Opciones;
using ReservaSitio.Services.Opciones;
using ReservaSitio.Repository.Opcion;
using ReservaSitio.Abstraction.IApplication.Perfiles;
using ReservaSitio.Abstraction.IService.Perfiles;
using ReservaSitio.Abstraction.IRepository.Perfiles;
using ReservaSitio.Abstraction.IApplication.Usuario;
using ReservaSitio.Abstraction.IService.Usuario;
using ReservaSitio.Application.Usuario;
using ReservaSitio.Services.Usuario;
using ReservaSitio.Repository.Usuario;
using ReservaSitio.Abstraction.IApplication.LogError;
using ReservaSitio.Abstraction.IService.LogError;
using ReservaSitio.Abstraction.IRepository.LogError;
using ReservaSitio.Application.LogError;
using ReservaSitio.Services.LogError;
using ReservaSitio.Repository.Log_Error;
using ReservaSitio.Abstraction.IApplication.Empresa;
using ReservaSitio.Abstraction.IService.Empresa;
using ReservaSitio.Application.Empresa;
using ReservaSitio.Services.Empresa;
using ReservaSitio.Repository.Empresa;
using ReservaSitio.Abstraction.IRepository.Empresa;
using ReservaSitio.Abstraction.IApplication.ParametrosAplicacion;
using ReservaSitio.Services.ParametrosAplicacion;
using ReservaSitio.Application.ParametrosAplicacion;
using ReservaSitio.Repository.ParametrosAplicacion;
using ReservaSitio.Abstraction.IService.ParametrosAplicacion;
using ReservaSitio.Abstraction.IRepository.ParametroAplicacion;

namespace ReservaSitio.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var fglfrom = Configuration["flagPeticionFrontLocal"];
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                        
                        Title = "ReservaSitio.API",
                        Version = "v1",
                        Description = "Api by Cummins platform.",
                    //TermsOfService = new Uri("https://sample.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Valtx Technology Team",
                        Email = "contacto@vatx.pe",
                        Url = new Uri("https://www.valtx.pe/"),
                    }
                });
                
            });


            /**/
            if (fglfrom == "1") {
                services.AddCors(opt =>
                {
                    opt.AddPolicy(name: _policyName, builder =>
                    {
                        builder.WithOrigins(
                            "*")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                });
            } else {
                services.AddCors(opt =>
                {
                    opt.AddPolicy(name: _policyName, builder =>
                    {
                        builder.WithOrigins( "http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .AllowAnyMethod();
                    });
                });
            }
            

            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CS_ReservaSitio"),
                b => b.MigrationsAssembly("ReservaSitio.API"))
            );

            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApiDbContext>();

            #region "agregar claces"
            //services.AddScoped(typeof(IApplication<>), typeof(Application<>));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IDBContext<>), typeof(ApiDbContext<>));
            services.AddScoped(typeof(ITokenHandlerService), typeof(TokenHandlerService));
            //services.AddScoped(typeof(ICaptchaGoogleService), typeof(CaptchaGoogleService));
            services.AddScoped(typeof(IAutenticacion), typeof(autenticacionLogic));
            services.AddScoped<ICustomConnection, CustomConnection>(_ => new CustomConnection(Configuration["ConnectionStrings:CS_ReservaSitio"]));

         

            
            services.AddScoped<ILogErrorAplication, LogErrorAplication>();
            services.AddScoped<ILogErrorServices, LogErrorServices>();
            services.AddScoped<ILogErrorRepository, LogErrorRepository>();

   /* */
            services.AddScoped<IEmpresaAplication, EmpresaAplication>();
            services.AddScoped<IEmpresaServices, EmpresaServices>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();

            services.AddScoped<ILocalAplication, LocalAplication>();
            services.AddScoped<ILocalServices, LocalServices>();
            services.AddScoped<ILocalRepository, LocalRepository>();

            services.AddScoped<IPisoAplication, PisoAplication>();
            services.AddScoped<IPisoServices, PisoServices>();
            services.AddScoped<IPisoRepository, PisoRepository>();
            

            services.AddScoped<IModuloAplication, ModuloAplication>();
            services.AddScoped<IModuloServices, ModuloServices>();
            services.AddScoped<IModuloRepository, ModuloRepository>();

            services.AddScoped<IPerfilAplication, PerfilAplication>();
            services.AddScoped<IPerfilServices, PerfilServices>();
            services.AddScoped<IPerfilRespository, PerfilRespository>();

            services.AddScoped<IPerfilOpcionAplication, PerfilOpcionAplication>();
            services.AddScoped<IPerfilOpcionServices, PerfilOpcionServices>();
            services.AddScoped<IPerfilOpcionRepository, PerfilOpcionRepository>();

            services.AddScoped<IOpcionAplication, OpcionAplication>();
            services.AddScoped<IOpcionServices, OpcionServices>();
            services.AddScoped<IOpcionRepository, OpcionRepository>();

            services.AddScoped<IUsuarioAplication, UsuarioAplication>();
            services.AddScoped<IUsuarioServices, UsuarioServices>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            services.AddScoped<IPlantillaCorreoAplication, PlantillaCorreoAplication>();
            services.AddScoped<IPlantillaCorreoServices, PlantillaCorreoServices>();
            services.AddScoped<IPlantillaCorreoRepository, PlantillaCorreoRepository>();

            services.AddScoped<IParametroAplication, ParametroAplication>();
            services.AddScoped<IParametroServices, ParametroServices>();
            services.AddScoped<IParametroRepository, ParametroRepository>();




            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


            services.AddScoped<ICaptchaGoogleApplication, CaptchaGoogleApplication>();
            services.AddScoped<ICaptchaGoogleService, CaptchaGoogleService>();


            services.AddScoped<IAzureBlobClientService, AzureBlobClientService>();

            #endregion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(options => {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"])),
                  ValidateIssuer = false,
                  ValidateAudience = false,
              };
          });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var fglfrom = Configuration["flagPeticionFrontLocal"];
            var swaggerBasePath = "";
            app.UseSwagger(c =>
            {
               // c.SerializeAsV2 = "";
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{swaggerBasePath}" } };
                });
            });


            /* if (env.IsDevelopment())
             {*/
            app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReservaSitio.API v1"));
           // }

            //app.UseCors(options => options.AllowAnyOrigin());
            app.UseCors(_policyName);
            app.UseHttpsRedirection();
            
            app.UseRouting();

            if (fglfrom == "1") {
                app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins(
                      "*"));
            }
            else
            {

                app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(
                 "http://localhost"));
            }
            
 

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
