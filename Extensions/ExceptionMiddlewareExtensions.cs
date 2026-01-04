using CompanyEmployees.ErrorModel;
using CompanyEmployees.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CompanyEmployees.Extensions
{
    public static class ExceptionMiddlewareExtensions  //static (xk intato non deve essere istanziata e contiene extension methods),  è un middleware custom
    {
        //global exception handler x tutta l'app

        public static void ConfigureExceptionHandler(this WebApplication app) {
            app.UseExceptionHandler(appError =>  //intercetta qualsiasi exception non gestita, funziona globalmente, override il comportamento di default
            {
                appError.Run(async context =>  //.Run() è il terminal(fine corsa) middleware, context è l'http context della req fallita
                {
                    //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";  //setti forzando il type di response, da fare x api rest. questo type lo vedi anche nell'header delle res su postman
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    //aspnetcore intercetta l'exception -> la salva in IExceptionHandlerFeature -> tu la recuperi qui e la salvi in contextFeature.  contextFeature.Error è quindi l'exception reale
                    if (contextFeature != null)  //security
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound, _ => StatusCodes.Status500InternalServerError
                            //se è NotFoundException (my custom .cs file) → 404,
                            //altrimenti → 500
                            //in questo separi errori business da errori di sistema!
                            //e.g. throw new CompanyNotFoundException(id); automaticamente diventa 404 Not Found   senza che devi scrivere return NotFound() nel controller.
                        };

                        await context.Response.WriteAsync( new ErrorDetails()  //crea obj ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode, //setta 
                            Message = contextFeature.Error.Message,  //setta
                        }.ToString());  //serializza in json
                        /*e.g. cosa riceve infine il client
                         {
                            "statusCode": 404,
                            "message": "The company with id ... doesn't exist in the database."
                          }
                         */
                    }

                });
            });
        }

    }
}
