using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CompanyEmployees.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message) : base(message)
        {

        }


    }
}
