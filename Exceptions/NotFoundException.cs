using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CompanyEmployees.Exceptions
{
    public abstract class NotFoundException : Exception   //abstract, Exception is real exception in .net
    {
        protected NotFoundException(string message) : base(message)  //protected, chiamabile solo dai children
        {
        }
        //passa il mex al constr superclass (Exception)

    }
    //x gestire tutte le eccezioni NotFound in un unico punto, here!!
    /* e.g.
        throw new CompanyNotFoundException(id);
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
     */
}
