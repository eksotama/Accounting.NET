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
    public enum TAccount_EntryType
    {
        Credit,
        Debit
    }

    public class TAccount_Entry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public TAccount_EntryType Type { get; set; }
        
        public Guid TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual TAccount Account { get; set; }

        public decimal Amount { get; set; }
        //public virtual Currency Currency { get; set; }
    }
}
