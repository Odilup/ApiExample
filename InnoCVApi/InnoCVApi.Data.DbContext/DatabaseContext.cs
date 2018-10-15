using System.Data.Entity;
using InnoCVApi.Domain.Entities.Users;

namespace InnoCVApi.Data.DbContext
{
    public class DatabaseContext : BaseDbContext
    {
        public DatabaseContext(string databaseConnectionString) : base(databaseConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(new DatabaseContextInitializer(databaseConnectionString));
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}