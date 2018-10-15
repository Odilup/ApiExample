using System;
using System.Data.Entity;
using InnoCVApi.Domain.Entities.Users;

namespace InnoCVApi.Data.DbContext
{
    /// <summary>
    /// Database initialization class to insert some test data in the database
    /// </summary>
    public class DatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        private readonly string _databaseConnectionString;

        public DatabaseContextInitializer(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString;
        }
        protected override void Seed(DatabaseContext context)
        {
            using (var ctx = new DatabaseContext(_databaseConnectionString))
            {
                ctx.Users.Add(new User
                {
                    Id = 1,
                    Name = "Pedro Pérez",
                    BirthDate = new DateTime(1977, 05, 10)
                });
                ctx.Users.Add(new User
                {
                    Id = 2,
                    Name = "Javier Pulido",
                    BirthDate = new DateTime(1981, 11, 10)
                });
                ctx.Users.Add(new User
                {
                    Id = 3,
                    Name = "Juan José Moreno",
                    BirthDate = new DateTime(1977, 04, 10)
                });

                    ctx.SaveChanges();
            }
        }
    }
}