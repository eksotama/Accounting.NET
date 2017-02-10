using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public enum TAccount_Type
    {
        None = 0,
        Assets,
        Liabilities,
        Expenses,
        Incomes
    }

    public interface IProcessableTAccount
    {
        string Number { get; set; }
        string Label { get; set; }
        List<TAccount_Entry> Entries { get; set; }
    }

    public class TAccountAggregated : IProcessableTAccount
    {
        [NotMapped]
        public List<TAccount_Entry> Entries { get; set; }

        [NotMapped]
        public string Label { get; set; }

        [NotMapped]
        public string Number { get; set; }
        

        public TAccountAggregated()
        {
            this.Entries = new List<TAccount_Entry>();
        }
    }

    public class TAccount : IValidatableObject, IProcessableTAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Label { get; set; }

        public Guid LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        [InverseProperty("Account")]
        public virtual List<TAccount_Entry> Entries { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (AccountingDbContext)validationContext.Items["Context"];
            var crud = (AccountingDbContextOperation)validationContext.Items["CRUD"];
            var obj = (TAccount)validationContext.ObjectInstance;

            var errors = new List<ValidationResult>();

            if (crud == AccountingDbContextOperation.Create || crud == AccountingDbContextOperation.Update)
            {
                if (obj.Ledger == null)
                {
                    "TAccount.Ledger.Empty".AddErrorMessage(context, errors);
                }
                if (string.IsNullOrWhiteSpace(obj.Number))
                {
                    "TAccount.Number.Empty".AddErrorMessage(context, errors);
                }
                if (string.IsNullOrWhiteSpace(obj.Label))
                {
                    "TAccount.Label.Empty".AddErrorMessage(context, errors);
                }
                if (obj.Number.Length < obj?.Ledger?.Depth)
                {
                    "TAccount.Number.LengthShorterThanLedgerDepth".AddErrorMessage(context, errors);
                }
                if (obj.Number.Length > obj?.Ledger?.Depth)
                {
                    "TAccount.Number.LengthLongerThanLedgerDepth".AddErrorMessage(context, errors);
                }
            }

            return errors;
        }
    }
}
