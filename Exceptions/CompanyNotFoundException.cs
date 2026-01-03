namespace CompanyEmployees.Exceptions
{
    public sealed class CompanyNotFoundException : NotFoundException  //sealed, no children
    {
        public CompanyNotFoundException(Guid companyId)
            : base($"The company with id: {companyId} doesn't exist in the database.")
        {
        }
        //passa il mex al constr superclass (NotFoundException)

    }
}
