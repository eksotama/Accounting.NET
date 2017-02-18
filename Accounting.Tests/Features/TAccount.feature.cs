﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AccountingNET.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class TAccountFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "TAccount", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "TAccount")))
            {
                AccountingNET.Tests.Features.TAccountFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table1.AddRow(new string[] {
                        "Name",
                        "MyLedger"});
            table1.AddRow(new string[] {
                        "Depth",
                        "3"});
            testRunner.Given("I create a ledger \"L\" with the properties", ((string)(null)), table1, "Given ");
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - NF")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        public virtual void TAccount_CreateATAccount_NF()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Create a TAccount - NF", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table2.AddRow(new string[] {
                        "Ledger",
                        "MyLedger"});
            table2.AddRow(new string[] {
                        "Number",
                        "123"});
            table2.AddRow(new string[] {
                        "Label",
                        "My Account"});
            testRunner.When("I create a TAccount \"T1\" with the properties", ((string)(null)), table2, "When ");
            testRunner.Then("I receive this ok message: \"TAccount.Created.Ok\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            this.ScenarioCleanup();
        }
        
        public virtual void TAccount_CreateATAccount_MandatoryPropertiesMissing(string description, string ledger, string number, string label, string message, string[] exampleTags)
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Create a TAccount - Mandatory properties missing", exampleTags);
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table3.AddRow(new string[] {
                        "Ledger",
                        string.Format("{0}", ledger)});
            table3.AddRow(new string[] {
                        "Number",
                        string.Format("{0}", number)});
            table3.AddRow(new string[] {
                        "Label",
                        string.Format("{0}", label)});
            testRunner.When("I create a TAccount \"T1\" with the properties", ((string)(null)), table3, "When ");
            testRunner.Then(string.Format("I receive this error message: \"{0}\"", message), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - Mandatory properties missing: Ledger")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Ledger")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Description", "Ledger")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Ledger", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Number", "123")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Label", "My Account")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Message", "TAccount.Ledger.Empty")]
        public virtual void TAccount_CreateATAccount_MandatoryPropertiesMissing_Ledger()
        {
            this.TAccount_CreateATAccount_MandatoryPropertiesMissing("Ledger", "", "123", "My Account", "TAccount.Ledger.Empty", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - Mandatory properties missing: Number")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Number")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Description", "Number")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Ledger", "MyLedger")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Number", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Label", "My Account")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Message", "TAccount.Number.Empty")]
        public virtual void TAccount_CreateATAccount_MandatoryPropertiesMissing_Number()
        {
            this.TAccount_CreateATAccount_MandatoryPropertiesMissing("Number", "MyLedger", "", "My Account", "TAccount.Number.Empty", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - Mandatory properties missing: Label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Description", "Label")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Ledger", "MyLedger")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Number", "123")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Label", "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Message", "TAccount.Label.Empty")]
        public virtual void TAccount_CreateATAccount_MandatoryPropertiesMissing_Label()
        {
            this.TAccount_CreateATAccount_MandatoryPropertiesMissing("Label", "MyLedger", "123", "", "TAccount.Label.Empty", ((string[])(null)));
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - Number is shorter that ledger depth")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        public virtual void TAccount_CreateATAccount_NumberIsShorterThatLedgerDepth()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Create a TAccount - Number is shorter that ledger depth", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table4.AddRow(new string[] {
                        "Ledger",
                        "MyLedger"});
            table4.AddRow(new string[] {
                        "Number",
                        "12"});
            table4.AddRow(new string[] {
                        "Label",
                        "My Account"});
            testRunner.When("I create a TAccount \"T1\" with the properties", ((string)(null)), table4, "When ");
            testRunner.Then("I receive this ok message: \"TAccount.Created.Ok\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Create a TAccount - Number is longer that ledger depth")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        public virtual void TAccount_CreateATAccount_NumberIsLongerThatLedgerDepth()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Create a TAccount - Number is longer that ledger depth", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Property",
                        "Value"});
            table5.AddRow(new string[] {
                        "Ledger",
                        "MyLedger"});
            table5.AddRow(new string[] {
                        "Number",
                        "1234"});
            table5.AddRow(new string[] {
                        "Label",
                        "My Account"});
            testRunner.When("I create a TAccount \"T1\" with the properties", ((string)(null)), table5, "When ");
            testRunner.Then("I receive this error message: \"TAccount.Number.LengthLongerThanLedgerDepth\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Sum a TAccount - NF")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        public virtual void TAccount_SumATAccount_NF()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Sum a TAccount - NF", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Number",
                        "Ledger",
                        "Label"});
            table6.AddRow(new string[] {
                        "100",
                        "MyLedger",
                        "Account 100"});
            table6.AddRow(new string[] {
                        "200",
                        "MyLedger",
                        "Account 200"});
            testRunner.Given("I create multiple TAccounts", ((string)(null)), table6, "Given ");
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Debit",
                        "Credit",
                        "Amount Debit",
                        "Amount Credit"});
            table7.AddRow(new string[] {
                        "100",
                        "",
                        "10.00",
                        ""});
            table7.AddRow(new string[] {
                        "",
                        "200",
                        "",
                        "10.00"});
            testRunner.And("I record a transaction \"TRANS1\" on ledger \"MyLedger\"", ((string)(null)), table7, "And ");
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Debit",
                        "Credit",
                        "Amount Debit",
                        "Amount Credit"});
            table8.AddRow(new string[] {
                        "100",
                        "",
                        "5.00",
                        ""});
            table8.AddRow(new string[] {
                        "",
                        "200",
                        "",
                        "5.00"});
            testRunner.And("I record a transaction \"TRANS2\" on ledger \"MyLedger\"", ((string)(null)), table8, "And ");
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Debit",
                        "Credit",
                        "Amount Debit",
                        "Amount Credit"});
            table9.AddRow(new string[] {
                        "200",
                        "",
                        "2.00",
                        ""});
            table9.AddRow(new string[] {
                        "",
                        "100",
                        "",
                        "2.00"});
            testRunner.And("I record a transaction \"TRANS3\" on ledger \"MyLedger\"", ((string)(null)), table9, "And ");
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Debit",
                        "Credit",
                        "Amount Debit",
                        "Amount Credit"});
            table10.AddRow(new string[] {
                        "200",
                        "",
                        "2.00",
                        ""});
            table10.AddRow(new string[] {
                        "",
                        "100",
                        "",
                        "2.00"});
            testRunner.And("I record a transaction \"TRANS4\" on ledger \"MyLedger\"", ((string)(null)), table10, "And ");
            testRunner.Then("the debit sum of the TAccount \"100\" on ledger \"MyLedger\" is \"15.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            testRunner.And("the credit sum of the TAccount \"100\" on ledger \"MyLedger\" is \"4.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit balance of the TAccount \"100\" on ledger \"MyLedger\" is \"11.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit balance of the TAccount \"100\" on ledger \"MyLedger\" is \"-11.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("TAccount - Sum an aggregated TAccount - NF")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "TAccount")]
        public virtual void TAccount_SumAnAggregatedTAccount_NF()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("TAccount - Sum an aggregated TAccount - NF", ((string[])(null)));
            this.ScenarioSetup(scenarioInfo);
            this.FeatureBackground();
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Number",
                        "Ledger",
                        "Label"});
            table11.AddRow(new string[] {
                        "100",
                        "MyLedger",
                        "Account 100"});
            table11.AddRow(new string[] {
                        "500",
                        "MyLedger",
                        "Account 500"});
            table11.AddRow(new string[] {
                        "501",
                        "MyLedger",
                        "Account 501"});
            table11.AddRow(new string[] {
                        "510",
                        "MyLedger",
                        "Account 510"});
            table11.AddRow(new string[] {
                        "511",
                        "MyLedger",
                        "Account 511"});
            testRunner.Given("I create multiple TAccounts", ((string)(null)), table11, "Given ");
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Debit",
                        "Credit",
                        "Amount Debit",
                        "Amount Credit"});
            table12.AddRow(new string[] {
                        "500",
                        "",
                        "10.00",
                        ""});
            table12.AddRow(new string[] {
                        "501",
                        "",
                        "10.00",
                        ""});
            table12.AddRow(new string[] {
                        "510",
                        "",
                        "10.00",
                        ""});
            table12.AddRow(new string[] {
                        "511",
                        "",
                        "10.00",
                        ""});
            table12.AddRow(new string[] {
                        "",
                        "100",
                        "",
                        "40.00"});
            testRunner.And("I record a transaction \"TRANS1\" on ledger \"MyLedger\"", ((string)(null)), table12, "And ");
            testRunner.Then("the debit sum of the TAccount \"50\" on ledger \"MyLedger\" is \"20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
            testRunner.And("the credit sum of the TAccount \"50\" on ledger \"MyLedger\" is \"0.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit balance of the TAccount \"50\" on ledger \"MyLedger\" is \"20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit balance of the TAccount \"50\" on ledger \"MyLedger\" is \"-20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit sum of the TAccount \"51\" on ledger \"MyLedger\" is \"20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit sum of the TAccount \"51\" on ledger \"MyLedger\" is \"0.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit balance of the TAccount \"51\" on ledger \"MyLedger\" is \"20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit balance of the TAccount \"51\" on ledger \"MyLedger\" is \"-20.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit sum of the TAccount \"5\" on ledger \"MyLedger\" is \"40.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit sum of the TAccount \"5\" on ledger \"MyLedger\" is \"0.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the debit balance of the TAccount \"5\" on ledger \"MyLedger\" is \"40.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            testRunner.And("the credit balance of the TAccount \"5\" on ledger \"MyLedger\" is \"-40.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
