using BaseLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ServerLibrary.Data
{
    public class AppDbContext : DbContext
    {
        // Konstruktor untuk DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet untuk masing-masing tabel
        public DbSet<Employee> Employees { get; set; }
        public DbSet<GeneralDepartement> GeneralDepartements { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<SystemRole> SystemRoles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


    }
}
