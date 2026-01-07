using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace CompanyEmployees.Repository.Contracts
{
    public interface IRepositoryBase<T>  //T quindi entity generica, adattibile per tutte
    {
        IQueryable<T> FindAll(bool trackChanges);  
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        /*
         - T  generico, può essere qualsiasi Entity (Company, Employee, ect)
         - IQueryable<T> è una query differita: contiene logica query ma non viene eseguita subito, puoi anche aggiungere Where, OrderBy, Include ect, l'sql viene generato realmente SOLO quando leggi realmente i dati (w ToList()/First()/Count()/ect) 
         - trackChanges : true (EF traccia le entità se cambi un campo e poi chiami SaveChanges(), EF aggiorna il DB automaticamente), false (no tracciamento, better performance, ottima x query di sola lettura!).
              var employee = repository.FindByCondition(e => e.Id == id, true).First();
              employee.Position = "Manager";
              repositoryContext.SaveChanges(); // EF aggiorna DB automaticamente
         - Expression<Func<T,bool>> permette query dinamiche, es. filtra con lambda.
         */

    }
}
