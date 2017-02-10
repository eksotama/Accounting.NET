using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.BLL
{
    public class MacroService
    {
        private AccountingDbContext Context { get; set; }

        public MacroService(AccountingDbContext context)
        {
            Context = context;
        }

        public Macro CreateMacro()
        {
            var l = new Macro();
            "Macro.Created.Ok".AddOkMessage(Context);
            Context.Macros.Add(l);
            return l;
        }

        public string RunScript(Macro m)
        {
            var scriptRunner = new MacroScriptRunner();
            return scriptRunner.RunScript(m.Script);
        }
    }
}
