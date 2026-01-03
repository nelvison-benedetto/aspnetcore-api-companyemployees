using CompanyEmployees.Configuration;
using CompanyEmployees.models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CompanyEmployees.Repository
{
    public class RepositoryContext : DbContext
    {
        //DbContext è il bridge code c# - Db. ogni DbContext è un 'contenitore' di tabs-view-queries, gestisce il tracking delle entities, genera sql, gestisce le config del modello (pk, relazioni, vincoli, lunghezze campi, ect)

        public RepositoryContext(DbContextOptions options) : base(options) {  
        }
        //DbContextOptions contiene configurazioni (tipo database, stringa connessione). inietti here w DI, base(options) chiam il constrct di DbContextOptions
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());  //SEE Configuration/CompanyConfiguration.cs x info details!!!
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
        //add(inietta w ApplyConfiguration()) regole custom (Fluent API)(e.g.pk composte,lenght strs,relations,vincoli,ect) salvate in un file (e.g.Configuration/CompanyConfiguration.cs) quando EF costruisce il modello della entity

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //DbSet<T> è tabella del db, e.g. EF capisce che Companies -> tab db Companies.
        //il DbSet è il punto di partenza per le query EF!

    }
}
