using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;

namespace Accounting.BLL
{
    public class MacroProxy
    {
        public MacroScriptRunner _runner { get; set; }
        public Ledger _ledger { get; set; } 
        public AccountingDbContext _context { get; set; }

        public MacroProxy(MacroScriptRunner runner, Ledger ledger, AccountingDbContext context)
        {
            _runner = runner;
            _ledger = ledger;
            _context = context;
        }

        public string call(string macroName)
        {
            var macro = _context.Macros.FirstOrDefault(o => o.Name == macroName);
            var result = _runner.RunScript(macro.Script, _ledger, _context);
            return result;
        }
    }
}
