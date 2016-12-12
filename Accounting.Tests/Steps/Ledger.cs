using Accounting.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Accounting.Tests
{
    [Binding]
    public class Ledger
    {
        private CommonContext cc { get; set; }

        public Ledger(CommonContext commonContext)
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
        }

    }
}
