using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;

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
    }
}
