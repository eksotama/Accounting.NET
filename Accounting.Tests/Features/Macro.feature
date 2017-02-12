Feature: Macro

Background:
	Given I create a ledger "L" with the properties
		| Property | Value    |
		| Name     | MyLedger |
		| Depth    | 3        |
	And I create a TAccount "T1" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Number   | 100         |
		| Label    | Account 100 |
	And I create a TAccount "T2" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Number   | 200         |
		| Label    | Account 200 |
	And I create a TAccount "T3" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Number   | 300         |
		| Label    | Account 300 |

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

