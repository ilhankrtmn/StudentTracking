using StudentTracking.Data.EntityFramework.Entities;
using StudentTracking.Data.Enums;
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
            optionsBuilder.UseSqlServer("server=DESKTOP-TLNMTIQ\\SQLEXPRESS;database=HearMeOut; integrated security=true;TrustServerCertificate=True;");
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public DbSet<GameConfiguration> GameConfigurations { get; set; }
        public DbSet<GameParticipationType> GameParticipationTypes { get; set; }
        public DbSet<GameTransaction> GameTransactions { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Game>().HasKey(p => p.GameID);
            modelBuilder.Entity<Game>().Property(p => p.Status).HasDefaultValue(0);
            modelBuilder.Entity<Game>().Property(p => p.StartDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Customer>().HasKey(p => p.CustomerID);
            modelBuilder.Entity<Customer>().Property(p => p.Createdate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Customer>().Property(p => p.Status).HasDefaultValue(0);

            modelBuilder.Entity<CustomerTransaction>().HasKey(p => p.TransactionId);
            modelBuilder.Entity<CustomerTransaction>().Property(p => p.Createdate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<GameConfiguration>().HasKey(p => p.ID);
            modelBuilder.Entity<GameConfiguration>().Property(p => p.Status).HasDefaultValue(0);

            modelBuilder.Entity<GameTransaction>().HasKey(p => p.EntryID);
            modelBuilder.Entity<GameTransaction>().Property(p => p.CreateDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<GameTransaction>().Property(p => p.Status).HasDefaultValue(GameTransactionEnum.Inactive);

            modelBuilder.Entity<Cities>().HasKey(p => p.CityID);
            modelBuilder.Entity<Cities>().Property(p => p.Priority).HasDefaultValue(0);

            modelBuilder.Entity<UserType>().HasKey(p => p.Id);

        }


        [DbFunction("JSON_VALUE", "dbo")]
        public static string JsonValue(string source, string path) => throw new NotSupportedException();

        [DbFunction("JSON_QUERY", "dbo")]
        public static string JsonQuery(string source, string path) => throw new NotSupportedException();

    }
}