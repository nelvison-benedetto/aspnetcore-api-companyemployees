using CompanyEmployees.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyEmployees.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>  //IEntityTypeConfiguration<T> interface che obbliga di definire il metodo Configure() 
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        { //metodo chiamato da EF durante OnModelCreating  nel DbContext, here puoi definire chiavi primarie, relazioni, vincoli, lunghezze campi, ecc.
            /*e.g.
            builder.HasKey(c => c.Id); // chiave primaria esplicita
            builder.Property(c => c.Name).IsRequired().HasMaxLength(60);
            builder.HasMany(c => c.Employees)
                   .WithOne(e => e.Company)
                   .HasForeignKey(e => e.CompanyId);
            */
            //però se non specifico queste cmnq EF usa convenzioni + DataAnnotations presenti nel modello Company.cs
            //in big prjs per si usa e.g. questo example commented cioe Fluent API per definire regole avanzate (pk composta, vincoli complessi, indici, ect)

            builder.HasData(  //questo serve solo x il seed (popolamento w fakes), in big prjs in .cs a parte il seeder
                new Company
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "IT_Solutions Ltd",
                    Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                    Country = "USA"
                },
                new Company
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Admin_Solutions Ltd",
                    Address = "312 Forest Avenue, BF 923",
                    Country = "USA"
                }

            );
            

        }

    }
}
