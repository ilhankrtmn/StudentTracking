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
        public DbSet<OutgoingMail> OutgoingMails { get; set; }
        public DbSet<UserEmailOtp> UserEmailOtps { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Absence> Absences { get; set; }


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
            modelBuilder.Entity<OutgoingMail>().Property(p => p.SendUserId).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.RecipientUserId).HasDefaultValue(0);
            modelBuilder.Entity<OutgoingMail>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<OutgoingMail>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.SendUserId);

            modelBuilder.Entity<UserEmailOtp>().HasKey(p => p.Id);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<UserEmailOtp>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Lesson>().HasKey(p => p.Id);
            modelBuilder.Entity<Lesson>().Property(p => p.CreatedDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Lesson>().Property(p => p.Status).HasDefaultValue(0);

            modelBuilder.Entity<Grade>().HasKey(p => p.Id);
            modelBuilder.Entity<Grade>().Property(p => p.MidtermGrade).HasDefaultValue(0);
            modelBuilder.Entity<Grade>().Property(p => p.FinalGrade).HasDefaultValue(0);
            modelBuilder.Entity<Grade>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.StudentId);

            modelBuilder.Entity<Absence>().HasKey(p => p.Id);
            modelBuilder.Entity<Absence>().Property(p => p.Count).HasDefaultValue(0);
            modelBuilder.Entity<Absence>().HasOne(p => p.User).WithMany().HasForeignKey(p => p.StudentId);
        }
    }
}