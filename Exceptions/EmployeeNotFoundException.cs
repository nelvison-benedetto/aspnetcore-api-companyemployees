namespace CompanyEmployees.Exceptions
{
    public sealed class EmployeeNotFoundException : NotFoundException  //sealed, no children
    {
        public EmployeeNotFoundException(Guid employeeId)
            : base($"The employee with id: {employeeId} doesn't exist in the database.")
        {
        }
        //passa il mex al constr superclass (NotFoundException)

    }
}
