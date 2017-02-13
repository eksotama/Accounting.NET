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
		| Number   | 123        |
		| Label    | My Account |
	Then I receive this ok message: "TAccount.Created.Ok"

Scenario Outline: TAccount - Create a TAccount - Mandatory properties missing
	When I create a TAccount "T1" with the properties
		| Property | Value    |
		| Ledger   | <Ledger> |
		| Number   | <Number> |
		| Label    | <Label>  |
	Then I receive this error message: "<Message>"

	Examples: 
	| Description | Ledger   | Number | Label      | Message               |
	| Ledger      |          | 123    | My Account | TAccount.Ledger.Empty |
	| Number      | MyLedger |        | My Account | TAccount.Number.Empty |
	| Label       | MyLedger | 123    |            | TAccount.Label.Empty  |

Scenario: TAccount - Create a TAccount - Number is shorter that ledger depth
	When I create a TAccount "T1" with the properties
		| Property | Value      |
		| Ledger   | MyLedger   |
		| Number   | 12         |
		| Label    | My Account |
	Then I receive this error message: "TAccount.Number.LengthShorterThanLedgerDepth"

Scenario: TAccount - Create a TAccount - Number is longer that ledger depth
	When I create a TAccount "T1" with the properties
		| Property | Value      |
		| Ledger   | MyLedger   |
		| Number   | 1234       |
		| Label    | My Account |
	Then I receive this error message: "TAccount.Number.LengthLongerThanLedgerDepth"

Scenario: TAccount - Sum a TAccount - NF
	Given I create multiple TAccounts
		| Number | Ledger   | Label       |
		| 100    | MyLedger | Account 100 |
		| 200    | MyLedger | Account 200 |
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

Scenario: TAccount - Sum an aggregated TAccount - NF
	Given I create multiple TAccounts
		| Number | Ledger   | Label       |
		| 100    | MyLedger | Account 100 |
		| 500    | MyLedger | Account 500 |
		| 501    | MyLedger | Account 501 |
		| 510    | MyLedger | Account 510 |
		| 511    | MyLedger | Account 511 |
	And I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 500   |        | 10.00        |               |
		| 501   |        | 10.00        |               |
		| 510   |        | 10.00        |               |
		| 511   |        | 10.00        |               |
		|       | 100    |              | 40.00         |
	Then the debit sum of the TAccount "50" on ledger "MyLedger" is "20.00"
	And the credit sum of the TAccount "50" on ledger "MyLedger" is "0.00"
	And the debit balance of the TAccount "50" on ledger "MyLedger" is "20.00"
	And the credit balance of the TAccount "50" on ledger "MyLedger" is "-20.00"
	And the debit sum of the TAccount "51" on ledger "MyLedger" is "20.00"
	And the credit sum of the TAccount "51" on ledger "MyLedger" is "0.00"
	And the debit balance of the TAccount "51" on ledger "MyLedger" is "20.00"
	And the credit balance of the TAccount "51" on ledger "MyLedger" is "-20.00"
	And the debit sum of the TAccount "5" on ledger "MyLedger" is "40.00"
	And the credit sum of the TAccount "5" on ledger "MyLedger" is "0.00"
	And the debit balance of the TAccount "5" on ledger "MyLedger" is "40.00"
	And the credit balance of the TAccount "5" on ledger "MyLedger" is "-40.00"

	
