namespace CompanyEmployees.Repository.Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }  //readonly
        IEmployeeRepository Employee { get; }  //readonly
        void Save();

    }
}
