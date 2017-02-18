using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Accounting.BLL;
using TechTalk.SpecFlow;
using FluentAssertions;
using Newtonsoft.Json;

namespace Accounting.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly CommonContext cc;

        public CommonSteps(CommonContext commonContext)
        {
            cc = commonContext;
        }

        private string GetAssemblyData(string resourceName)
        {
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            StreamReader sr = new StreamReader(s);
            string data = sr.ReadToEnd();
            sr.Close();
            s.Close();
            return data;
        }

        [BeforeScenario()]
        public void Setup()
        {
            cc.ResetContext();
            EffortProviderFactory.ResetDb();
        }

        [BeforeTestRun]
        public static void AssemblyInit()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [Then(@"I receive this ok message: ""(.*)""")]
        public void ThenIReceiveThisOkMessage(string id)
        {
            var m = cc.GetContext().Messages.FirstOrDefault(o => o.Id == id && o.Type == MessageType.Ok);
            m.Should().NotBeNull();
        }

        [Then(@"I receive this error message: ""(.*)""")]
        public void ThenIReceiveThisErrorMessage(string id)
        {
            var m = cc.GetContext().Messages.FirstOrDefault(o => o.Id == id && o.Type == MessageType.Error);
            m.Should().NotBeNull();
        }

        internal class tAccountFromFile
        {
            public string Number { get; set; }
            public string Label { get; set; }
        }

        [Given(@"I load t-accounts from file ""([a-zA-Z0-9\._-]+)"" on ledger ""(\w+)""")]
        public void GivenILoadT_AccountsFromFile(string resFile, string ledgerRef)
        {
            var ledger = (Ledger) cc.ObjectBag[$"ledger-{ledgerRef}"];
            cc.GetContext().Entry(ledger).Reload();

            var accountService = new TAccountService(cc.GetContext());

            var content = GetAssemblyData($"AccountingNET.Tests.Resources.TAccounts.{resFile}");
            var accounts = JsonConvert.DeserializeObject<List<tAccountFromFile>>(content);

            foreach (var a in accounts)
            {
                var c = accountService.CreateTAccount();
                c.Ledger = ledger;
                c.Label = a.Label;
                c.Number = a.Number;
            }

            cc.GetContext().SaveChanges();
        }

    }
}
