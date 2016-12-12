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

        public void AddEntryToTransaction(Transaction trans, TAccount debit, TAccount credit, decimal? debitAmount = null, decimal? creditAmount = null)
        {
            if (credit == null && debit == null)
            {
                "Transaction.Record.Account.Missing".AddErrorMessage(Context);
                return;
            }

            if (credit != null && debit != null)
            {
                "Transaction.Record.Account.OnlyOneAccountPerEntry".AddErrorMessage(Context);
                return;
            }

            if (credit != null && !creditAmount.HasValue)
            {
                "Transaction.Record.CreditAmount.Missing".AddErrorMessage(Context);
                return;
            }

            if (debit != null && !debitAmount.HasValue)
            {
                "Transaction.Record.DebitAmount.Missing".AddErrorMessage(Context);
                return;
            }
            
            var entry = new TAccount_Entry
            {
                Type = credit != null ? TAccount_EntryType.Credit : TAccount_EntryType.Debit,
                Account = credit != null ? credit : debit,
                Amount = credit != null ? creditAmount.Value : debitAmount.Value
            };
            trans.Entries.Add(entry);
        }
    }
}
