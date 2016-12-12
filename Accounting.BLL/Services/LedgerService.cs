using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.BLL
{
    public class LedgerService
    {
        private AccountingDbContext Context { get; set; }

        public LedgerService(AccountingDbContext context)
        {
            Context = context;
        }

        public Ledger CreateLedger()
        {
            var l = new Ledger();
            "Ledger.Created.Ok".AddOkMessage(Context);
            Context.Ledgers.Add(l);
            return l;
        }
    }
}
