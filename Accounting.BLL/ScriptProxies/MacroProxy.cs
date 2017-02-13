using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;
using IronPython.Runtime;

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

        public string call(string macroName, PythonDictionary parameters = null)
        {
            var macro = _context.Macros.FirstOrDefault(o => o.Name == macroName);
            var script = macro.Script;
            if (parameters != null)
            {
                var macroParameters = new MacroScriptParameters();
                foreach (var k in parameters.keys())
                {
                    macroParameters.SetParameter((string)k, (string)parameters.get((string)k));
                }
                script = macroParameters.ReplaceParameters(macro);
            }
            var result = _runner.RunScript(script, _ledger, _context);
            return result;
        }
    }
}
