using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.BLL
{
    public class TAccountService
    {
        private AccountingDbContext Context { get; set; }

        public TAccountService(AccountingDbContext context)
        {
            Context = context;
        }

        public TAccount CreateTAccount()
        {
            var t = new TAccount();
            "TAccount.Created.Ok".AddOkMessage(Context);
            Context.TAccounts.Add(t);
            return t;
        }

        public IProcessableTAccount GetAggregatedAccountByNumber(Ledger ledger, string accountNumber)
        {
            //var namedAccount = this.Context.TAccounts.FirstOrDefault(o => o.Number == accountNumber);
            //if (namedAccount != null && !namedAccount.CanAggregate)
            //{
            //    return namedAccount;
            //}
            var accounts = ledger.Accounts.Where(o => o.Number.StartsWith(accountNumber)).ToList();
            var result = new TAccountAggregated();
            result.Number = accountNumber;
            result.Label = "Aggregated Account - " + accountNumber;
            foreach(var a in accounts)
            {
                result.Entries.AddRange(a.Entries);
            }
            result.Entries.Sort((e1, e2) => e1.Transaction.Sequence.CompareTo(e1.Transaction.Sequence));
            return result;
        }

        private string CompleteAccountToFitLedger(Ledger ledger, string accountNumber)
        {
            if (accountNumber.Length < ledger.Depth)
            {
                var toFill = ledger.Depth - accountNumber.Length;
                for (var k = 0; k < toFill; k++)
                {
                    accountNumber += "_";
                } 
            }
            return accountNumber;
        }

        public TAccount GetAccountByNumber(Ledger ledger, string accountNumber = "")
        {
            // Reject empty account number
            if (ledger == null || string.IsNullOrWhiteSpace(accountNumber))
            {
                return null;
            }

            // Look for an existing account
            var namedAccount = this.Context.TAccounts.FirstOrDefault(o => o.Number == accountNumber);
            if (namedAccount != null)
            {
                return namedAccount;
            }

            // Build a virtual account otherwise
            var virtualAccount = CreateTAccount();
            virtualAccount.Label = $"virtual-{accountNumber}";
            virtualAccount.Ledger = ledger;
            virtualAccount.IsVirtual = true;
            virtualAccount.Number = CompleteAccountToFitLedger(ledger, accountNumber);
            return virtualAccount;
        }

        public decimal GetDebitSum(IProcessableTAccount account)
        {
            var result = 0.00M;
            foreach (var e in account.Entries)
            {
                if (e.Type == TAccount_EntryType.Debit)
                {
                    result += e.Amount;
                }
            }
            return result;
        }
        public decimal GetCreditSum(IProcessableTAccount account)
        {
            var result = 0.00M;
            foreach (var e in account.Entries)
            {
                if (e.Type == TAccount_EntryType.Credit)
                {
                    result += e.Amount;
                }
            }
            return result;
        }
        public decimal GetDebitBalance(IProcessableTAccount account)
        {
            var debit = this.GetDebitSum(account);
            var credit = this.GetCreditSum(account);
            return debit - credit;
        }
        public decimal GetCreditBalance(IProcessableTAccount account)
        {
            var debit = this.GetDebitSum(account);
            var credit = this.GetCreditSum(account);
            return credit - debit;
        }
    }
}
