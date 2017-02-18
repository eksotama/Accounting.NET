using Accounting.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accounting.DAL;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Accounting.Tests
{
    [Binding]
    public sealed class LedgerSteps
    {
        private CommonContext cc { get; set; }

        public LedgerSteps(CommonContext commonContext)
        {
            cc = commonContext;
        }

        [Given(@"I create a ledger ""(.*)"" with the properties")]
        [When(@"I create a ledger ""(.*)"" with the properties")]
        public void WhenICreateALedgerWithTheProperties(string reference, Table table)
        {
            var service = new LedgerService(cc.GetContext());
            var l = service.CreateLedger();

            foreach(var r in table.Rows)
            {
                var property = r["Property"];
                var value = r["Value"];

                switch(property)
                {
                    case "Name": l.Name = value; break;
                    case "Depth": l.Depth = int.Parse(value); break;
                }
            }

            cc.GetContext().SaveChanges();

            cc.ObjectBag["ledger-" + reference] = l;
        }

        [Then(@"the content of ledger ""(.*)"" is")]
        public void ThenTheContentOfLedgerIs(string reference, Table table)
        {
            var l = (Ledger) cc.ObjectBag["ledger-" + reference];
            cc.GetContext().Entry(l).Reload();

            foreach (var r in table.Rows)
            {
                var trans = int.Parse(r["Trans"]);
                var debit = r["Debit"];
                var credit = r["Credit"];
                
                var t = l.Transactions.FirstOrDefault(o => o.Sequence == trans);
                t.Should().NotBeNull();
                if (!string.IsNullOrWhiteSpace(debit))
                {
                    var dAmount = decimal.Parse(r["Debit Amount"]);
                    var a = t.Entries.FirstOrDefault(o => o.Account.OriginalNumber == debit);
                    a.Should().NotBeNull();
                    a.Amount.Should().Be(dAmount);
                }
                else
                {
                    var cAmount = decimal.Parse(r["Credit Amount"]);
                    var a = t.Entries.FirstOrDefault(o => o.Account.OriginalNumber == credit);
                    a.Should().NotBeNull();
                    a.Amount.Should().Be(cAmount);
                }
            }
        }

    }
}
