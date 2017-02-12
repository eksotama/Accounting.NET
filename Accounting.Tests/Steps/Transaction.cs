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
    public sealed class TransactionSteps
    {
        private CommonContext cc { get; set; }

        public TransactionSteps(CommonContext commonContext)
        {
            cc = commonContext;
        }

        [Given(@"I record a transaction ""(.*)"" on ledger ""(.*)""")]
        [When(@"I record a transaction ""(.*)"" on ledger ""(.*)""")]
        public void WhenIRecordATransactionOnLedger(string reference, string ledgerName, Table table)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            var transactionService = new TransactionService(this.cc.GetContext());
            var trans = transactionService.CreateTransaction(ledger);

            foreach(var r in table.Rows)
            {
                var creditStr = r["Credit"];
                var debitStr = r["Debit"];
                var creditAmountStr = r["Amount Credit"];
                var debitAmountStr = r["Amount Debit"];
                decimal? creditAmount = null;
                decimal? debitAmount = null;
                if (!string.IsNullOrWhiteSpace(creditAmountStr))
                {
                    creditAmount = decimal.Parse(creditAmountStr);
                }
                if (!string.IsNullOrWhiteSpace(debitAmountStr))
                {
                    debitAmount = decimal.Parse(debitAmountStr);
                }

                transactionService.AddEntryToTransaction(
                    trans,
                    this.cc.GetContext().TAccounts.FirstOrDefault(o => o.Number == debitStr),
                    this.cc.GetContext().TAccounts.FirstOrDefault(o => o.Number == creditStr),
                    debitAmount,
                    creditAmount
                );
            }

            this.cc.GetContext().SaveChanges();

            this.cc.ObjectBag["transaction-" + reference] = trans;
        }

        [Then(@"the number of transactions on ledger ""(.*)"" is ""(.*)""")]
        public void ThenTheNumberOfTransactionsOnLedgerIs(string ledgerName, int expected)
        {
            var ledger = this.cc.GetContext().Ledgers.FirstOrDefault(o => o.Name == ledgerName);
            ledger.Transactions.Count().Should().Be(expected);
        }

        [Then(@"the number of entries is ""(.*)"" for transaction ""(.*)""")]
        public void ThenTheNumberOfEntriesIsForTransaction(int expected, string transReference)
        {
            var trans = (Transaction)this.cc.ObjectBag["transaction-" + transReference];
            trans.Entries.Count().Should().Be(expected);
        }

        [Then(@"the number of debit entries is ""(.*)"" for transaction ""(.*)""")]
        public void ThenTheNumberOfDebitEntriesIsForTransaction(int expected, string transReference)
        {
            var trans = (Transaction)this.cc.ObjectBag["transaction-" + transReference];
            trans.Entries.Count(o => o.Type == TAccount_EntryType.Debit).Should().Be(expected);
        }

        [Then(@"the number of credit entries is ""(.*)"" for transaction ""(.*)""")]
        public void ThenTheNumberOfCreditEntriesIsForTransaction(int expected, string transReference)
        {
            var trans = (Transaction)this.cc.ObjectBag["transaction-" + transReference];
            trans.Entries.Count(o => o.Type == TAccount_EntryType.Credit).Should().Be(expected);
        }

        [Then(@"the transaction ""(.*)"" is balanced")]
        public void ThenTheTransactionIsBalanced(string transReference)
        {
            var trans = (Transaction)this.cc.ObjectBag["transaction-" + transReference];
            var service = new TransactionService(cc.GetContext());
            var balanced = service.IsTransactionBalanced(trans);
            balanced.Should().BeTrue();
        }

    }
}
