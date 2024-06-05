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
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<OutgoingMail> OutgoingMails { get; set; }
        public virtual DbSet<UserEmailOtp> UserEmailOtps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasKey(p => p.CustomerID);
            modelBuilder.Entity<Customer>().Property(p => p.Createdate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Customer>().Property(p => p.Status).HasDefaultValue(0);

            modelBuilder.Entity<Cities>().HasKey(p => p.CityID);
            modelBuilder.Entity<Cities>().Property(p => p.Priority).HasDefaultValue(0);

            modelBuilder.Entity<User>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<UserType>().HasKey(p => p.Id);

            modelBuilder.Entity<OutgoingMail>().HasKey(p => p.Id);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.UserId).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<UserEmailOtp>().HasKey(p => p.Id);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);
        }
    }
}