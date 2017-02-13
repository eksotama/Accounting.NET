using Accounting.BLL;
using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Accounting.Tests
{
    [Binding]
    public sealed class MacroSteps
    {
        private CommonContext cc { get; set; }

        public MacroSteps(CommonContext commonContext)
        {
            cc = commonContext;
        }

        [Given(@"I create a macro ""(.*)"" with the properties")]
        [When(@"I create a macro ""(.*)"" with the properties")]
        public void WhenICreateAMacroWithTheProperties(string reference, Table table)
        {
            var service = new MacroService(cc.GetContext());
            var m = service.CreateMacro();

            foreach (var r in table.Rows)
            {
                var property = r["Property"];
                var value = r["Value"];

                switch (property)
                {
                    case "Name": m.Name = value; break;
                    case "Description": m.Description = value; break;
                    case "Script": m.Script = value; break;
                }
            }

            cc.GetContext().SaveChanges();

            cc.ObjectBag["macro-" + reference] = m;
        }

        [Given(@"I update the script of the macro ""(.*)"" to")]
        public void GivenIUpdateTheScriptOfTheMacroTo(string reference, string multilineText)
        {
            var m = (Macro) cc.ObjectBag["macro-" + reference];
            cc.GetContext().Entry(m).Reload();
            m.Script = multilineText;
            cc.GetContext().SaveChanges();
        }

        [When(@"I execute the macro ""(\w+)"" on ledger ""(\w+)"" into result ""(.*)""")]
        public void WhenIExecuteTheMacroIntoResult(string reference, string ledgerReference, string resultReference)
        {
            var m = (Macro)cc.ObjectBag["macro-" + reference];
            var l = (Ledger)cc.ObjectBag["ledger-" + ledgerReference];
            cc.GetContext().Entry(m).Reload();
            cc.GetContext().Entry(l).Reload();
            var service = new MacroService(cc.GetContext());
            var result = service.RunScript(m, l);

            cc.GetContext().SaveChanges();

            cc.ObjectBag["macroResult-" + resultReference] = result;
        }

        [When(@"I execute the macro ""(.*)"" on ledger ""(.*)"" into result ""(.*)"" with parameters")]
        public void WhenIExecuteTheMacroOnLedgerIntoResultWithParameters(string reference, string ledgerReference, string resultReference, Table table)
        {
            var m = (Macro)cc.ObjectBag["macro-" + reference];
            var l = (Ledger)cc.ObjectBag["ledger-" + ledgerReference];
            cc.GetContext().Entry(m).Reload();
            cc.GetContext().Entry(l).Reload();
            var service = new MacroService(cc.GetContext());
            
            var parameters = new MacroScriptParameters();
            foreach (var r in table.Rows)
            {
                parameters.SetParameter(r["Name"], r["Value"]);
            }
            var result = service.RunScript(m, l, parameters);

            cc.GetContext().SaveChanges();

            cc.ObjectBag["macroResult-" + resultReference] = result;
        }


        [Then(@"the macro result ""(.*)"" is ""(.*)""")]
        public void ThenTheMacroResultIs(string reference, string expected)
        {
            var result = cc.ObjectBag["macroResult-" + reference] as string;
            result.Should().Be(expected);
        }

    }
}
