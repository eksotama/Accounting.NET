using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;

namespace Accounting.BLL
{
    public class LedgerProxy
    {
        public Ledger _ledger { get; set; }
        public AccountingDbContext _context { get; set; }

        public LedgerProxy(Ledger ledger, AccountingDbContext context)
        {
            _ledger = ledger;
            _context = context;
        }

        public TransactionProxy T()
        {
            var tService = new TransactionService(_context);
            var t = tService.CreateTransaction(_ledger);
            var tProxy = new TransactionProxy(t, _context);
            return tProxy;
        }
    }
}
