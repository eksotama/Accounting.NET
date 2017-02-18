using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;

namespace Accounting.BLL
{
    public class TransactionProxy
    {
        public Transaction _transaction { get; set; }
        public AccountingDbContext _context { get; set; }

        public TransactionProxy(Transaction transaction, AccountingDbContext context)
        {
            _transaction = transaction;
            _context = context;
        }

        public void D(string account, decimal value)
        {
            var tService = new TransactionService(_context);
            var tAccountService = new TAccountService(_context);
            //var tAccount = _transaction.Ledger.Accounts.FirstOrDefault(o => o.Number == account);
            var tAccount = tAccountService.GetAccountByNumber(_transaction.Ledger, account);
            tService.AddEntryToTransaction(_transaction, tAccount, null, value, null);
        }

        public void C(string account, decimal value)
        {
            var tService = new TransactionService(_context);
            var tAccountService = new TAccountService(_context);
            //var tAccount = _transaction.Ledger.Accounts.FirstOrDefault(o => o.Number == account);
            var tAccount = tAccountService.GetAccountByNumber(_transaction.Ledger, account);
            tService.AddEntryToTransaction(_transaction, null, tAccount, null, value);
        }
    }
}
