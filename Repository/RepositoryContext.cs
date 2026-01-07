using CompanyEmployees.Configuration;
using CompanyEmployees.models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CompanyEmployees.Repository
{
    public class RepositoryContext : DbContext
    {
        //DbContext è il bridge code c# - Db. DbContext rappresenta una sessione con il database, tiene traccia delle entity (Change Tracker), coordina le query e le operazioni CRUD, gestisce le config del modello (pk, relazioni, vincoli, lunghezze campi, ect), usa il modello EF per tradurre LINQ in SQL

        public RepositoryContext(DbContextOptions options) : base(options) {  
        }
        //DbContextOptions contiene le configurazioni del DbContext (provider, connection string, ecc.), viene iniettato tramite DI e passato al costruttore base di DbContext

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());  //SEE Configuration/CompanyConfiguration.cs x info details!!!
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
        //OnModelCreating viene eseguito durante la fase di costruzione del modello EF, ApplyConfiguration applica le configurazioni Fluent API definite separatamente (chiavi, relazioni, vincoli, lunghezze campi, ecc.) attualmente nei files configurations/CompanyConfiguration.cs  configurations/EmployeeConfiguration.cs

        //salvate 
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //DbSet<T> è tabella del db, e.g. EF capisce che Companies -> tab db Companies.
        //il DbSet è il punto di partenza per le query EF!

    }
}
