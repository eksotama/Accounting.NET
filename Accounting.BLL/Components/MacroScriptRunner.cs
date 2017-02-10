using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting;

namespace Accounting.BLL
{
    public class MacroScriptRunner
    {
        public string RunScript(string script)
        {
            script = script + "\n\n__result = __main()\n__result";
            var en = Python.CreateEngine();
            var scope = en.CreateScope();

            // add variable _ledger...

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
