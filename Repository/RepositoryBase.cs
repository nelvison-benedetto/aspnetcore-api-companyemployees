using CompanyEmployees.Repository.Contracts;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CompanyEmployees.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class  //abstract, vincolo T deve essere un reference type (here class)(EF funziona solo con reference types come Entity)!!
    {
        protected RepositoryContext repositoryContext;  //protected, xk deve essere accessibile anche ai children
        public RepositoryBase(RepositoryContext repositoryContext) {  //DependencyInjection (DI)
            this.repositoryContext = repositoryContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? repositoryContext.Set<T>().AsNoTracking() :
            repositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool trackChanges) =>
                  !trackChanges ? 
                repositoryContext.Set<T>().Where(expression).AsNoTracking() :
                  repositoryContext.Set<T>().Where(expression);

        public void Create(T entity)=> repositoryContext.Set<T>().Add(entity);

        public void Delete(T entity)=> repositoryContext.Set<T>().Remove(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);


    }
}
