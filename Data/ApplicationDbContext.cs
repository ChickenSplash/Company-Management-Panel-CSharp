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
                new Company { Id = 1, Name = "Tech Corp", Email = "info@techcorp.com", LogoPath ="placeholder.png", Website = "https://techcorp.com" },
                new Company { Id = 2, Name = "Health Inc", Email = "contact@healthinc.com", LogoPath = "placeholder.png", Website = "https://healthinc.com" }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Alice", LastName = "Johnson", CompanyId = 1, Email = "alice@techcorp.com", PhoneNumber = "07775845772" },
                new Employee { Id = 2, FirstName = "Bob", LastName = "Smith", CompanyId = 1, Email = "bob@techcorp.com", PhoneNumber = "07830018563" },
                new Employee { Id = 3, FirstName = "Carol", LastName = "Davis", CompanyId = 2, Email = "carol@healthinc.com", PhoneNumber="07774750114" }
            );
        }
    }
}
