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
            !trackChanges ? 
            repositoryContext.Set<T>().AsNoTracking() : repositoryContext.Set<T>();
            //a seconda di che bool gli passi, setta Yes o No il trackingChanges

        public IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool trackChanges) =>
                  !trackChanges ? 
                repositoryContext.Set<T>().Where(expression).AsNoTracking() :
                  repositoryContext.Set<T>().Where(expression);
        //filtra solo entities che soddifano la condition(expression) e a seconda di che bool gli passi setta Yes o No il trackingChanges. 
        //repositoryContext.Set<T>() -> rappresenta tutte le righe della tabella T, .Where(expression) -> costruisce un sql usando 'where' (non filtra subito in memory), .AsNoTracking() -> EF quando materializza gli obj NON li traccia, infine ritorna Ritorna IQueryable<T> ma la query non è ancora eseguita!! viene eseguita quando fai e.g. var entities = repository.FindByCondition(...).ToList(); FirstOrDefault() ect

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        //EF fa: aggancia l’entity al DbContext -> imposta il suo EntityState = Added -> la mette nel Change Tracker. //DbSet<T> è una facciata (facade) verso il database
        //quando fai repositoryContext.SaveChanges() EF fa: guarda il Change Tracker -> trova tutte le entity con stato Added -> genera SQL (INSERT INTO Table (...) VALUES (...)) -> Esegue la query sul DB!

        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);


    }
}
