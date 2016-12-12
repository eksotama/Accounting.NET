using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public class Ledger : IValidatableObject
    {
        public Ledger()
        {
            this.Transactions = new List<Transaction>();
            this.Accounts = new List<TAccount>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Depth { get; set; }

        [InverseProperty("Ledger")]
        public virtual List<Transaction> Transactions { get; set; }

        [InverseProperty("Ledger")]
        public virtual List<TAccount> Accounts { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (AccountingDbContext)validationContext.Items["Context"];
            var crud = (AccountingDbContextOperation)validationContext.Items["CRUD"];
            var obj = (Ledger)validationContext.ObjectInstance;

            var errors = new List<ValidationResult>();

            if(crud == AccountingDbContextOperation.Create || crud == AccountingDbContextOperation.Update)
            {
                if(string.IsNullOrWhiteSpace(obj.Name))
                {
                    "Ledger.Name.Empty".AddErrorMessage(context, errors);
                }
                if (obj.Depth <= 0)
                {
                    "Ledger.Depth.MustBeAPositiveNumber".AddErrorMessage(context, errors);
                }
            }

            return errors;
        }
    }
}
