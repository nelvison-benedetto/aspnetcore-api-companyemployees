
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

            // Add services to the container.
            builder.Services.ConfigureCors();  //added
            builder.Services.ConfigureRepositoryManager();  //added
            builder.Services.ConfigureServiceManager();  //added
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.AddAutoMapper(typeof(Program)); //added
                //mappatura auto
            //supress apicontroller behaviour - stop auto validation
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                //obj che lo stato dei model la validita dei dati
            });


            builder.Services.AddControllers();
            builder.Services.AddAuthentication();
            builder.Services.ConfigureJWT(builder.Configuration);  //added


            //now abbiamo settato tutti i settings x la build!
            //#########

            var app = builder.Build();
            app.ConfigureExceptionHandler();  //added


            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


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

            app.UseCors("CorsPolicy");  //added
            app.MapControllers();


            app.Run();
        }
    }
}
