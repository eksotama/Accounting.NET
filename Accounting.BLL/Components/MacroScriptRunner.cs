using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;
using IronPython.Hosting;
using Microsoft.Scripting;

namespace Accounting.BLL
{
    public class MacroScriptRunner
    {
        public string RunScript(string script, Ledger ledger, AccountingDbContext context)
        {
            script = script + "\n\n__result = __main()\n__result";
            var en = Python.CreateEngine();
            var scope = en.CreateScope();

            // add variable _ledger...
            var lProxy = new LedgerProxy(ledger, context);
            var mProxy = new MacroProxy(this, ledger, context);
            scope.SetVariable("_ledger", lProxy);
            scope.SetVariable("_macro", mProxy);

            var result = "";
            try
            {
                var source = en.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
                source.Execute(scope);
                var __result = scope.GetVariable("__result");
                if (__result is int)
                {
                    result = ((int) __result).ToString();
                }
                else if (__result is double)
                {
                    result = ((double) __result).ToString("F2");
                }
                else
                {
                    result = (string) __result;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
