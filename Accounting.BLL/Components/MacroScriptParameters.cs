using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;

namespace Accounting.BLL
{
    public class MacroScriptParameters
    {
        private Dictionary<string, string> _parameters { get; set; }

        public MacroScriptParameters()
        {
            _parameters = new Dictionary<string, string>();
        }

        public void SetParameter(string key, string value)
        {
            _parameters[key] = value;
        }

        public string ReplaceParameters(Macro m)
        {
            var script = m.Script;
            foreach (var kv in _parameters)
            {
                script = script.Replace($"__{kv.Key}__", kv.Value);
            }
            return script;
        }
    }
}
