using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace Accounting.DAL
{
    public enum AccountingDbContextOperation
    {
        Create,
        Delete,
        Update,
        Undefined
    }

    public class AccountingDbContext : DbContext
    {
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Ledger> Ledgers { get; set; }
        public virtual DbSet<TAccount> TAccounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Macro> Macros { get; set; }

        public List<Message> Messages { get; set; }

        public AccountingDbContext(DbConnection connection) : base(connection, true)
        {
            Messages = new List<Message>();
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            // Set CRUD operation
            items["CRUD"] = AccountingDbContextOperation.Undefined;
            if (entityEntry.State == EntityState.Added)
            {
                items["CRUD"] = AccountingDbContextOperation.Create;
            }
            if (entityEntry.State == EntityState.Deleted)
            {
                items["CRUD"] = AccountingDbContextOperation.Delete;
            }
            if (entityEntry.State == EntityState.Modified)
            {
                items["CRUD"] = AccountingDbContextOperation.Update;
            }

            // Set Context
            items["Context"] = this;

            if (entityEntry.Entity is IValidatableObject)
            {
                ((IValidatableObject)entityEntry.Entity).Validate(new ValidationContext(entityEntry.Entity, items));
            }

            return base.ValidateEntity(entityEntry, items);
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry)
        {
            return entityEntry.State == EntityState.Added 
                || entityEntry.State == EntityState.Modified 
                || entityEntry.State == EntityState.Deleted;
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                return 0;
            }
        }
    }
}
