
using CompanyEmployees.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //legge appsettings.json, appsettings.{Environment}.json, configura logging, prepara DI container. here NON PARTE ancora il server.

            // Add services to the container.
            builder.Services.ConfigureCors();  //run method in extensions/ServiceExtension.cs  (è un custom METHOD EXTENSION)
            builder.Services.ConfigureRepositoryManager();  //run method in extensions/ServiceExtension.cs
            builder.Services.ConfigureServiceManager();  //run method in extensions/ServiceExtension.cs
            builder.Services.ConfigureSqlContext(builder.Configuration);  //run method in extensions/ServiceExtension.cs
            builder.Services.AddAutoMapper(typeof(Program)); //mappatura auto, cerca mapping/MappingProfile.cs nell'assembly e registra IMapper


            builder.Services.Configure<ApiBehaviorOptions>(options =>  
            {
                options.SuppressModelStateInvalidFilter = true;
                //obj che lo stato dei model la validita dei dati
            });
            //di default .net se ModelState NON è valido -> ritorna 400 BadRequest senza entrare nel controller action! pero nei progetti reali vogliamo piu controllo su questo comportamento, e.g. vuoi restituire un 422 UnprocessableEntity o 400 o vuoi loggare l'errore, o vuoi restituire un formato di errore personalizzato, ect.
            //QUINDI QUA DISATTIVI QUESTA VALIDAZIONE AUTOMATICA DI DEFAULT (options.SuppressModelStateInvalidFilter = true;) + nel controller fai if (!ModelState.IsValid) {return UnprocessableEntity(ModelState)};  //UnprocessableEntity è un example
            //!!alternativa che invece mettere if(!ModelState.IsValid) ovunque, usa IActionFilter
            /*
             public class ValidationFilterAttribute : IActionFilter
            {
                public void OnActionExecuting(ActionExecutingContext context)
                {
                    if (!context.ModelState.IsValid)
                    {
                        context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                    }
                }
                public void OnActionExecuted(ActionExecutedContext context) { }
            }
            e poi nel controller
            [ServiceFilter(typeof(ValidationFilterAttribute))]
            public class EmployeesController : ControllerBase
             */


            builder.Services.AddControllers();  //registra MVC Controllers, abilita routing attributi [HttpGet],[HttpPost],ect
            builder.Services.AddAuthentication();  //registra i servizi di autenticazione nel DI (ma non configura nessun schema)
            builder.Services.ConfigureJWT(builder.Configuration);  //run method in extensions/ServiceExtension.cs

            //ora abbiamo settato tutti i settings x la build!

            var app = builder.Build();  //configuriamo l'app, ma non è ancora in ascolto
            
            app.ConfigureExceptionHandler();  //run method in extensions/ExceptionMiddlewareExtensions.cs

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();  //forza https (se arriva http redirect a https)
            app.UseStaticFiles();  //serve file statici direttamente dal server, senza passare dai controller
            app.UseRouting();  //decide quale endpoint deve gestire la request. in .net moderno è gia integrato dentro MapControllers()/MapGet(), ma cmnq sempre metterlo x chiarezza & compatibilita.

            app.UseAuthentication();  //legge le credenziali della req, costruisce utente HttpContext.User 
            app.UseAuthorization();  //DECIDE se l’utente può accedere alla risorsa, lavora w [Authorize] [Authorize(Roles = "admin")] [Authorize(Policy = "MyPolicy")] ect

            //MIDDLEWARES
            //test again mi sa che questi middleware non sono nella giusta posizione della pipeline
            //app.Use(async (context, next) => {
            //    Console.WriteLine("logic before executing Next level");
            //    await next.Invoke();
            //    Console.WriteLine("logic after executing Next level");
            //});
            //app.Map("/usinmapbranch", builder =>  //questo url finale
            //{
            //    builder.Use(async (context, next) =>
            //    {
            //        Console.WriteLine("map branch logic before executing Next level");
            //        await next.Invoke();
            //        Console.WriteLine("map branch logic after executing Next level");
            //    });
            //    builder.Run(async context =>
            //    {
            //        Console.WriteLine("map branch writing response to client");
            //        //context.Response.StatusCode = 200;
            //        await context.Response.WriteAsync("map branch Startup from webapi");
            //    });
            //});
            //app.Run(async context =>
            //{
            //    Console.WriteLine("writing response to client");
            //    //context.Response.StatusCode = 200;
            //    await context.Response.WriteAsync("Startup from webapi");
            //});

            app.UseCors("CorsPolicy");  //usa cors nominata 'CorsPolicy' extensions/ServiceExtensions.cs

            app.MapControllers();  //mappa [Route] [HttpGet] [HttpPost], senza questo 404 ovunque

            app.Run();  //avvio server


            /*
             POSTMAN
                https://localhost:5001/api/companies    //GET companies ok
                https://localhost:5001/api/companies/3d490a70-94ce-4d15-9494-5248280c2ce3   //GET single company ok
                https://localhost:5001/api/companies/c9d4c053-49b6-410c-bc78-2d54a9991870/employees   //GET all employees of target company ok
                https://localhost:5001/api/companies/c9d4c053-49b6-410c-bc78-2d54a9991870/employees/86dba8c0-d178-41e7-938c-ed49778fb52c   //GET target employee of target company (ma prima questo employee devo crearlo)

                https://localhost:5001/api/companies/43148C69-B6A1-45FF-B88E08DE3F05018D/employees
             */

        }
    }
}
