using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.BLL
{
    public class TransactionService
    {
        private AccountingDbContext Context { get; set; }

        public TransactionService(AccountingDbContext context)
        {
            Context = context;
        }

        public Transaction CreateTransaction(Ledger ledger)
        {
            var t = new Transaction();
            t.Ledger = ledger;
            "Transaction.Recorded.Ok".AddOkMessage(Context);
            Context.Transactions.Add(t);
            return t;
        }

        public bool IsTransactionBalanced(Transaction trans)
        {
            System.Nullable<decimal> sumDebit = trans.Entries.Where(o => o.Type == TAccount_EntryType.Debit).Sum(o => o.Amount);
            System.Nullable<decimal> sumCredit = trans.Entries.Where(o => o.Type == TAccount_EntryType.Credit).Sum(o => o.Amount);
            return sumDebit == sumCredit;
        }
    }
}
