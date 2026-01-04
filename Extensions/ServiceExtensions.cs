using CompanyEmployees.Repository;
using CompanyEmployees.Repository.Contracts;
using CompanyEmployees.Service;
using CompanyEmployees.Service.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions  //static (xk intato non deve essere istanziata e contiene extension methods), x raggruppare le config tecniche e tenere program.cs pulito!
    {
        //methd extension
        public static void ConfigureCors(this IServiceCollection services) {
            Console.WriteLine("here in ConfigureCors()");
            services.AddCors (options =>  //registri il servizio cors, ora aspnetcore sa cosa è il cors  method che vuole blocco di codice in input
            {
                options.AddPolicy("CorsPolicy", builder =>  //definisci policy con nome
                {
                    builder
                    .AllowAnyOrigin()  //accetta richieste da qualsiasi dominio
                    .AllowAnyMethod()  //GET,POST,PUT,DELETE
                    .AllowAnyHeader(); //Authorization, Content-Type, ecc
                    //ora le porte CORS sono aperte a tutti accettati token/tutto/ect, ovviamente in production non sara x tutti.
                });
            });
        } //grazie a 'this', aggiungiamo nuovo servizio a quelli gia presenti auth/ect, ora puoi puoi scrivere in program.cs 'builder.Services.' e ti suggerira anche 'ConfigureCors()' !!s

        public static void ConfigureRepositoryManager(this IServiceCollection services) {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            //quando aspnetcore vede IRepositoryManager crea RepositoryManager una volta per request.
            //AddScoped è lifetime good x EF. gli altri lifetime sono addTransient, addSingleton.
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            //quando aspnetcore vede IServiceManager crea ServiceManager una volta per request.
            //AddScoped è lifetime good x EF. gli altri lifetime sono addTransient, addSingleton.
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts => { opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")); });
            //legge appsettings.json -> usa SQL Server -> crea RepositoryContext per DI! questo è per il runtime DbContext (è simile a quello per (solo) le migration ContextFactory/ContextFactory.cs)
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) { 
            var jwtSettings = configuration.GetSection("JwtSettings");  //catch section in appsettings.json
            var secretKey = "BusinessSecretKeyBusinessSecretKeyBusinessSecretKeyBusinessSecretKey"; //only x trial, in production INFO
            services.AddAuthentication(opt =>  //imposti JWT come sistema di auth predefinito
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  //'usa jwt x auth'
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  //'usa jwt quando serve una challenge (401)'
            })
            .AddJwtBearer(options =>  //config how validare il token
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,  //activate (il token deve essere emesso da chi dici tu)
                    ValidateAudience = true,  //activate
                    ValidateLifetime = true,  //activate
                    ValidateIssuerSigningKey = true,  //activate
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    //usa la stessa key usata in AuthenticationService, se la key è diversa allora token non valido.
                };
            });

            /*
             ConfigureCors	            Sicurezza cross-domain
             ConfigureRepositoryManager	Repository
             ConfigureServiceManager	Business logic
             ConfigureSqlContext	    Database
             ConfigureJWT	            Autenticazione JWT
             */

        }

    }
}
