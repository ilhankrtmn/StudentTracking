using StudentTracking.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentTracking.Data.EntityFramework
{
    public class StudentTrackingContext : DbContext
    {
        public StudentTrackingContext(DbContextOptions<StudentTrackingContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-748VRTV;database=StudentTracking; integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(p => p.CustomerID);
            modelBuilder.Entity<Customer>().Property(p => p.Createdate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Customer>().Property(p => p.Status).HasDefaultValue(0);

            modelBuilder.Entity<Cities>().HasKey(p => p.CityID);
            modelBuilder.Entity<Cities>().Property(p => p.Priority).HasDefaultValue(0);

            modelBuilder.Entity<UserType>().HasKey(p => p.Id);
        }
    }
}