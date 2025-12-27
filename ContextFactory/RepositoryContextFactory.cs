using CompanyEmployees.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyEmployees.ContextFactory
{
    //runnata 1 volta, setta tutto 
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>  //added
    {
        public RepositoryContext CreateDbContext(string[] args) {
            //throw new NotImplementedException();

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //quindi in appsettings ora possiamo prendere la strig di connection

            var builder = new DbContextOptionsBuilder<RepositoryContext>().UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("CompanyEmployees"));

            return new RepositoryContext(builder.Options);
            
            //ora possiamo works w code-first cioe tramite classi .cs crei le tabelle del db
        }
    }
}
