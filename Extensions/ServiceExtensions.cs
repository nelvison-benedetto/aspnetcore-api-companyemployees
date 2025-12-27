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
    public static class ServiceExtensions  //added static, xk qua dentro mettiamo le utility
    {
        //methd extension
        public static void ConfigureCors(this IServiceCollection services) { //aggiungiamo funzionalita servizio nuovo aggiunto a quelli gia presenti auth/ect
            Console.WriteLine("here in ConfigureCors()");
            services.AddCors (options =>  //method che vuole blocco di codice in input
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    //ora le porte CORS sono aperte a tutti! accetti token/tutto
                });
            });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            //addTransient, addSingleton 
            
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();

        }

        //just x db, per il runtime (x la dependency injection!!)
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts => { opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")); });

        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration) { 
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = "BusinessSecretKeyBusinessSecretKeyBusinessSecretKeyBusinessSecretKey";
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true, //audience è il destinatario verso chi puoi spendere il token
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),

                };
            });


        }

    }
}
