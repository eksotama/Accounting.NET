Feature: Macro

Background:
	Given I create a ledger "L" with the properties
		| Property | Value    |
		| Name     | MyLedger |
		| Depth    | 3        |
	And I create multiple TAccounts
		| Number | Ledger   | Label       |
		| 100    | MyLedger | Account 100 |
		| 200    | MyLedger | Account 200 |
		| 300    | MyLedger | Account 300 |

#### CREATE

Scenario: Macro - Create a macro - NF
	When I create a macro "M" with the properties
		| Property    | Value          |
		| Name        | Macro1         |
		| Description | My description |
		| Script      | my script      |
	Then I receive this ok message: "Macro.Created.Ok"

Scenario Outline:  Macro - Create a macro - Mandatory properties missing
	When I create a macro "M" with the properties
		| Property | Value   |
		| Name     | <Name>  |
	Then I receive this error message: "<Message>"

	Examples: 
	| Description | Name | Message          |
	| Name        |      | Macro.Name.Empty |

#### SCRIPT

Scenario Outline: Macro - Define script - Hard coded value
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return <Input>
		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR"
	Then the macro result "MR" is "<Output>"

	Examples:
	| Description     | Input        | Output |
	| String          | '20'         | 20     |
	| String (Concat) | '20' + '20'  | 2020   |
	| Int             | 20           | 20     |
	| Int (Add)       | 20 + 1       | 21     |
	| Int (Substract) | 20 - 1       | 19     |
	| Int (Multiply)  | 2 * 3        | 6      |
	| Int (Divide)    | 10 / 5       | 2      |
	| Decimal         | 10.50        | 10.50  |
	| Decimal (Add)   | 10.50 + 0.50 | 11.00  |

Scenario: Macro - Define script - Parameters
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return __X__ + __Y__
		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR" with parameters
		| Name | Value |
		| X    | 10.00 |
		| Y    | 5.00  |
	Then the macro result "MR" is "15.00"


Scenario: Macro - Define script - Call another macro
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return 10.00
		#enddef
		"""
	And I create a macro "M2" with the properties
		| Property | Value  |
		| Name     | Macro2 |
	And I update the script of the macro "M2" to 
		"""
		def __main():
			return _macro.call('Macro1')
		#enddef
		"""
	When I execute the macro "M2" on ledger "L" into result "MR"
	Then the macro result "MR" is "10.00"

Scenario: Macro - Define script - Call another macro with parameters
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return __X__ + __Y__
		#enddef
		"""
	And I create a macro "M2" with the properties
		| Property | Value  |
		| Name     | Macro2 |
	And I update the script of the macro "M2" to 
		"""
		def __main():
			return _macro.call('Macro1', { 'X':'10.00', 'Y':'5.00' })
		#enddef
		"""
	When I execute the macro "M2" on ledger "L" into result "MR"
	Then the macro result "MR" is "15.00"

Scenario: Macro - Define script - Call another macro with parameters - forward parameters
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return __X__ + __Y__
		#enddef
		"""
	And I create a macro "M2" with the properties
		| Property | Value  |
		| Name     | Macro2 |
	And I update the script of the macro "M2" to 
		"""
		def __main():
			return _macro.call('Macro1', { 'X':'__A__', 'Y':'__B__' })
		#enddef
		"""
	When I execute the macro "M2" on ledger "L" into result "MR" with parameters
		| Name | Value |
		| A    | 10.00 |
		| B    | 5.00  |
	Then the macro result "MR" is "15.00"

Scenario: Macro - Define script - Add a transaction
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			t = _ledger.T()
			t.D('100', 5.00)
			t.D('200', 5.00)
			t.C('300', 10.00)
		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR"
	Then the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 5.00  |        |             |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 5.00  |        |             |
	And the content of the TAccount "300" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 10.00  | 1           |

Scenario: Macro - Define script - Add a transaction with parameters
	Given I create a macro "M" with the properties
		| Property | Value  |
		| Name     | Macro1 |
	And I update the script of the macro "M" to 
		"""
		def __main():
			t = _ledger.T()
			t.__CD1__('100', __Var1__)
			t.__CD2__('200', __Var2__)
			t.__CD3__('300', __Var3__)
		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR" with parameters
		| Name | Value |
		| Var1 | 5.00  |
		| CD1  | D     |
		| Var2 | 5.00  |
		| CD2  | D     |
		| Var3 | 10.00 |
		| CD3  | C     |
	Then the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 5.00  |        |             |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 5.00  |        |             |
	And the content of the TAccount "300" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 10.00  | 1           |
