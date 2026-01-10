using CompanyEmployees.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyEmployees.ContextFactory
{
    //x design-time, non serve a run-time. usata quando lanci Add-Migration Update-Databases 
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>  //added
    {
        public RepositoryContext CreateDbContext(string[] args) {  //EF chiama questo automaticamente
            //throw new NotImplementedException();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            //carica appsettings.json (che contiene la db connection string)

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("CompanyEmployees"));
            //specifici che Db è SQL Server, dici di usare al connection str called 'sqlConnection', specifici che le migratios stanno in CompanyEmployees

            return new RepositoryContext(builder.Options);
            //ora EF leggere il modello, generare migrations, creare Db (CODE-FIRST (ma x big prjs meglio DB-FIRST (lo fai sempre con EF oppure dapper(x pro) )!!))
            
        }
    }
}
