using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace CompanyEmployees.Repository.Contracts
{
    public interface IRepositoryBase<T>  //xk lo voglio sia x Company che per Employee
    {
        IQueryable<T> FindAll(bool trackChanges);  //iqueryable better than inumerable!!
        //possiamo tracciare i cambiamenti in c#!!
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
