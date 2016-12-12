Feature: TAccount

Background:
	Given I create a ledger "L" with the properties
		| Property | Value    |
		| Name     | MyLedger |
		| Depth    | 3        |

#### CREATE

Scenario: TAccount - Create a TAccount - NF
	When I create a TAccount "T1" with the properties
		| Property | Value      |
		| Ledger   | MyLedger   |
		| Type     | Assets     |
		| Number   | 123        |
		| Label    | My Account |
	Then I receive this ok message: "TAccount.Created.Ok"

Scenario Outline: TAccount - Create a TAccount - Mandatory properties missing
	When I create a TAccount "T1" with the properties
		| Property | Value    |
		| Ledger   | <Ledger> |
		| Type     | <Type>   |
		| Number   | <Number> |
		| Label    | <Label>  |
	Then I receive this error message: "<Message>"

	Examples: 
	| Description | Ledger   | Type   | Number | Label      | Message               |
	| Ledger      |          | Assets | 123    | My Account | TAccount.Ledger.Empty |
	| Type        | MyLedger |        | 123    | My Account | TAccount.Type.Empty   |
	| Number      | MyLedger | Assets |        | My Account | TAccount.Number.Empty |
	| Label       | MyLedger | Assets | 123    |            | TAccount.Label.Empty  |

Scenario: TAccount - Create a TAccount - Number is shorter that ledger depth
	When I create a TAccount "T1" with the properties
		| Property | Value      |
		| Ledger   | MyLedger   |
		| Type     | Assets     |
		| Number   | 12         |
		| Label    | My Account |
	Then I receive this error message: "TAccount.Number.LengthShorterThanLedgerDepth"

Scenario: TAccount - Create a TAccount - Number is longer that ledger depth
	When I create a TAccount "T1" with the properties
		| Property | Value      |
		| Ledger   | MyLedger   |
		| Type     | Assets     |
		| Number   | 1234       |
		| Label    | My Account |
	Then I receive this error message: "TAccount.Number.LengthLongerThanLedgerDepth"

Scenario: TAccount - Sum a TAccount - NF
	Given I create a TAccount "T1" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 100         |
		| Label    | Account 100 |
	And I create a TAccount "T2" with the properties
		| Property | Value       |
		| Ledger   | MyLedger   |
		| Type     | Assets      |
		| Number   | 200         |
		| Label    | Account 200 |
	And I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 10.00        |               |
		|       | 200    |              | 10.00         |
	And I record a transaction "TRANS2" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 5.00         |               |
		|       | 200    |              | 5.00          |
	And I record a transaction "TRANS3" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 200   |        | 2.00         |               |
		|       | 100    |              | 2.00          |
	And I record a transaction "TRANS4" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 200   |        | 2.00         |               |
		|       | 100    |              | 2.00          |
	Then the debit sum of the TAccount "100" on ledger "MyLedger" is "15.00"
	And the credit sum of the TAccount "100" on ledger "MyLedger" is "4.00"
	And the debit balance of the TAccount "100" on ledger "MyLedger" is "11.00"
	And the credit balance of the TAccount "100" on ledger "MyLedger" is "-11.00"

	
