using Accounting.BLL;
using Accounting.DAL;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Accounting.Tests.Steps
{
    [Binding]
    public sealed class TAccountSteps
    {
        private CommonContext cc { get; set; }

        public TAccountSteps(CommonContext commonContext)
        {
            cc = commonContext;
        }

        [Given(@"I create a TAccount ""(.*)"" with the properties")]
        [When(@"I create a TAccount ""(.*)"" with the properties")]
        public void WhenICreateTheT_AccountWithTheProperties(string reference, Table table)
        {
            var service = new TAccountService(cc.GetContext());
            var account = service.CreateTAccount();
            foreach(var r in table.Rows)
            {
                var property = r["Property"];
                var value = r["Value"];

                switch(property)
                {
                    case "Ledger":
                        {
                            var ledger = cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == value);
                            account.Ledger = ledger;
                        }
                        break;
                    case "Number":
                        {
                            account.Number = value;
                        }
                        break;
                    case "Label":
                        {
                            account.Label = value;
                        }
                        break;
                }
            }
            cc.GetContext().SaveChanges();
        }

        [Then(@"the content of the TAccount ""(.*)"" on ledger ""(.*)"" is")]
        public void ThenTheContentOfTheTAccountIs(string accountNumber, string ledgerName, Table table)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var service = new TAccountService(this.cc.GetContext());
            var account = service.GetAggregatedAccountByNumber(ledger, accountNumber);            

            foreach(var r in table.Rows)
            {
                var transDebit = r["TransDebit"];
                var transCredit = r["TransCredit"];
                var debitAmount = r["Debit"];
                var creditAmount = r["Credit"];

                if (!string.IsNullOrWhiteSpace(transDebit))
                {
                    var transNb = int.Parse(transDebit);
                    var trans = account.Entries.First(o => o.Transaction.Sequence == transNb);
                    trans.Type.Should().Be(TAccount_EntryType.Debit);
                    trans.Amount.Should().Be(decimal.Parse(debitAmount));
                }

                if (!string.IsNullOrWhiteSpace(transCredit))
                {
                    var transNb = int.Parse(transCredit);
                    var trans = account.Entries.First(o => o.Transaction.Sequence == transNb);
                    trans.Type.Should().Be(TAccount_EntryType.Credit);
                    trans.Amount.Should().Be(decimal.Parse(creditAmount));
                }
            }
        }

        [Then(@"the debit sum of the TAccount ""(.*)"" on ledger ""(.*)"" is ""(.*)""")]
        public void ThenTheDebitSumOfTheTAccountOnLedgerIs(string accountNumber, string ledgerName, Decimal expected)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var service = new TAccountService(this.cc.GetContext());
            var account = service.GetAggregatedAccountByNumber(ledger, accountNumber);
            var amount = service.GetDebitSum(account);
            amount.Should().Be(expected);
        }

        [Then(@"the credit sum of the TAccount ""(.*)"" on ledger ""(.*)"" is ""(.*)""")]
        public void ThenTheCreditSumOfTheTAccountOnLedgerIs(string accountNumber, string ledgerName, Decimal expected)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var service = new TAccountService(this.cc.GetContext());
            var account = service.GetAggregatedAccountByNumber(ledger, accountNumber);
            var amount = service.GetCreditSum(account);
            amount.Should().Be(expected);
        }

        [Then(@"the debit balance of the TAccount ""(.*)"" on ledger ""(.*)"" is ""(.*)""")]
        public void ThenTheDebitBalanceOfTheTAccountOnLedgerIs(string accountNumber, string ledgerName, Decimal expected)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var service = new TAccountService(this.cc.GetContext());
            var account = service.GetAggregatedAccountByNumber(ledger, accountNumber);
            var amount = service.GetDebitBalance(account);
            amount.Should().Be(expected);
        }

        [Then(@"the credit balance of the TAccount ""(.*)"" on ledger ""(.*)"" is ""(.*)""")]
        public void ThenTheCreditBalanceOfTheTAccountOnLedgerIs(string accountNumber, string ledgerName, Decimal expected)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var service = new TAccountService(this.cc.GetContext());
            var account = service.GetAggregatedAccountByNumber(ledger, accountNumber);
            var amount = service.GetCreditBalance(account);
            amount.Should().Be(expected);
        }


    }
}
