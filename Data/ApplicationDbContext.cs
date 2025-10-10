using Company_Management_Panel_CSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_Management_Panel_CSharp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Companies
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Tech Corp", Email = "info@techcorp.com", LogoPath = null, Website = "https://techcorp.com" },
                new Company { Id = 2, Name = "Health Inc", Email = "contact@healthinc.com", LogoPath = null, Website = "https://healthinc.com" }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                // Generated data using AI
                // Company 1 - TechCorp
                new Employee { Id = 1, FirstName = "Alice", LastName = "Johnson", CompanyId = 1, Email = "alice@techcorp.com", PhoneNumber = "07775845772" },
                new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", CompanyId = 1, Email = "bob@techcorp.com", PhoneNumber = "07830018563" },
                new Employee { Id = 3, FirstName = "Daniel", LastName = "Wright", CompanyId = 1, Email = "daniel@techcorp.com", PhoneNumber = "07765123987" },
                new Employee { Id = 4, FirstName = "Ella", LastName = "Brown", CompanyId = 1, Email = "ella@techcorp.com", PhoneNumber = "07798104563" },
                new Employee { Id = 5, FirstName = "Frank", LastName = "Thompson", CompanyId = 1, Email = "frank@techcorp.com", PhoneNumber = "07819245670" },
                new Employee { Id = 6, FirstName = "Grace", LastName = "Anderson", CompanyId = 1, Email = "grace@techcorp.com", PhoneNumber = "07770135842" },
                new Employee { Id = 7, FirstName = "Harry", LastName = "Lewis", CompanyId = 1, Email = "harry@techcorp.com", PhoneNumber = "07804329871" },
                new Employee { Id = 8, FirstName = "Isla", LastName = "Turner", CompanyId = 1, Email = "isla@techcorp.com", PhoneNumber = "07766129854" },

                // Company 2 - HealthInc
                new Employee { Id = 9, FirstName = "Carol", LastName = "Davis", CompanyId = 2, Email = "carol@healthinc.com", PhoneNumber = "07774750114" },
                new Employee { Id = 10, FirstName = "Jack", LastName = "Miller", CompanyId = 2, Email = "jack@healthinc.com", PhoneNumber = "07801563472" },
                new Employee { Id = 11, FirstName = "Katie", LastName = "Wilson", CompanyId = 2, Email = "katie@healthinc.com", PhoneNumber = "07894216053" },
                new Employee { Id = 12, FirstName = "Liam", LastName = "Moore", CompanyId = 2, Email = "liam@healthinc.com", PhoneNumber = "07787520169" },
                new Employee { Id = 13, FirstName = "Mia", LastName = "Taylor", CompanyId = 2, Email = "mia@healthinc.com", PhoneNumber = "07831654729" },
                new Employee { Id = 14, FirstName = "Noah", LastName = "Clark", CompanyId = 2, Email = "noah@healthinc.com", PhoneNumber = "07790765231" },
                new Employee { Id = 15, FirstName = "Olivia", LastName = "Martin", CompanyId = 2, Email = "olivia@healthinc.com", PhoneNumber = "07762594318" }
            );
        }
    }
}
