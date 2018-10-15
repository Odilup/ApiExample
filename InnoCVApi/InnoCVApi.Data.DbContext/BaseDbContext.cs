using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using InnoCVApi.Core.Resources;

namespace InnoCVApi.Data.DbContext
{
    public abstract class BaseDbContext : System.Data.Entity.DbContext
    {
        protected BaseDbContext(string databaseConnectionString) : base(databaseConnectionString)
        {
        }

        public override int SaveChanges()
        {
            var saveChanges = int.MinValue;

            try
            {
                saveChanges = base.SaveChanges();
            }
            catch (DbEntityValidationException dbValidationEx)
            {
                var validationErrors =
                    dbValidationEx.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors);
                var errors = string.Empty;

                foreach (var validationError in validationErrors)
                {
                    var propertyName = validationError.PropertyName;
                    var errorMessage = validationError.ErrorMessage;

                    var property = $"Property \"{propertyName}\": \"{errorMessage}\";";

                    errors += property;
                }

                var exMessage = string.Format(CultureInfo.InvariantCulture,
                    CoreDomainStringResources.Error_EntityValidationFailed, errors);

                throw new DbEntityValidationException(exMessage);
            }


            return saveChanges;

        }

        public override Task<int> SaveChangesAsync()
        {
            return Task.Run(() => SaveChanges());
        }

        public virtual int RollbackChanges()
        {
            var changes = 0;

            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            foreach (var entry in modifiedEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.State = EntityState.Detached;
                    changes++;
                }
                else
                {
                    entry.Reload();
                    changes++;
                }
            }

            return changes;
        }

        public virtual Task<int> RollbackChangesAsync()
        {
            return Task.Run(() => RollbackChanges());
        }
    }
}