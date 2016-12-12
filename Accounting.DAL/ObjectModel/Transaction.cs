using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public class Transaction
    {
        public Transaction()
        {
            this.Entries = new List<TAccount_Entry>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sequence { get; set; }

        public Guid LedgerId { get; set; }
        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        public DateTimeOffset TransactionDate { get; set; }

        [InverseProperty("Transaction")]
        public virtual List<TAccount_Entry> Entries { get; set; }
    }
}
