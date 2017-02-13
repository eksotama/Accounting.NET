Feature: Transaction

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
		| 500    | MyLedger | Account 500 |
		| 501    | MyLedger | Account 501 |
		| 510    | MyLedger | Account 510 |
		| 511    | MyLedger | Account 511 |

Scenario: Transaction - Record a transaction - NF
	When I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 50.00        |               |
		|       | 200    |              | 50.00         |
	Then I receive this ok message: "Transaction.Recorded.Ok"
	And the number of transactions on ledger "MyLedger" is "1"
	And the number of entries is "2" for transaction "TRANS1"
	And the number of debit entries is "1" for transaction "TRANS1"
	And the number of credit entries is "1" for transaction "TRANS1"
	And the transaction "TRANS1" is balanced
	And the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 50.00 |        |             |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 50.00  | 1           |

Scenario Outline: Transaction - Record a transaction - Mandatory properties missing
	When I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit   | Credit   | Amount Debit  | Amount Credit  |
		| <Debit> | <Credit> | <AmountDebit> | <AmountCredit> |
	Then I receive this error message: "<Message>"

	Examples:
	| Description                | Debit | Credit | AmountDebit | AmountCredit | Message                                           |
	| Account missing            |       |        | 10.00       |              | Transaction.Record.Account.Missing                |
	| Only one account per entry | 100   | 200    | 10.00       |              | Transaction.Record.Account.OnlyOneAccountPerEntry |
	| Debit amount missing       | 100   |        |             |              | Transaction.Record.DebitAmount.Missing            |
	| Credit amount missing      |       | 100    |             |              | Transaction.Record.CreditAmount.Missing           |

Scenario: Transaction - Record a transaction - NF - Multiple accounts on debit side
	When I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 25.00        |               |
		| 300   |        | 25.00        |               |
		|       | 200    |              | 50.00         |
	Then I receive this ok message: "Transaction.Recorded.Ok"
	And the number of transactions on ledger "MyLedger" is "1"
	And the number of entries is "3" for transaction "TRANS1"
	And the number of debit entries is "2" for transaction "TRANS1"
	And the number of credit entries is "1" for transaction "TRANS1"
	And the transaction "TRANS1" is balanced
	And the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 25.00 |        |             |
	And the content of the TAccount "300" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 25.00 |        |             |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 50.00  | 1           |

Scenario: Transaction - Record a transaction - NF - Multiple accounts on credit side
	When I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 50.00        |               |
		|       | 200    |              | 25.00         |
		|       | 300    |              | 25.00         |
	Then I receive this ok message: "Transaction.Recorded.Ok"
	And the number of transactions on ledger "MyLedger" is "1"
	And the number of entries is "3" for transaction "TRANS1"
	And the number of debit entries is "1" for transaction "TRANS1"
	And the number of credit entries is "2" for transaction "TRANS1"
	And the transaction "TRANS1" is balanced
	And the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 50.00 |        |             |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 25.00  | 1           |
	And the content of the TAccount "300" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		|            |       | 25.00  | 1           |

Scenario: Transaction - Record multiple transactions - NF
	When I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 50.00        |               |
		|       | 200    |              | 50.00         |
	And I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 200   |        | 25.00        |               |
		|       | 100    |              | 25.00         |
	Then the content of the TAccount "100" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 50.00 | 25.00  | 2           |
	And the content of the TAccount "200" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 2          | 25.00 | 50.00  | 1           |

Scenario: Transaction - Recorded transactions on sub-accounts are aggregated at upper levels when in the same transaction
	Given I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 500   |        | 10.00        |               |
		| 501   |        | 10.00        |               |
		| 510   |        | 10.00        |               |
		| 511   |        | 10.00        |               |
		|       | 100    |              | 40.00         |
	Then the content of the TAccount "50" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 |        |             |
		| 1          | 10.00 |        |             |
	And the content of the TAccount "51" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 |        |             |
		| 1          | 10.00 |        |             |
	And the content of the TAccount "5" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 |        |             |
		| 1          | 10.00 |        |             |
		| 1          | 10.00 |        |             |
		| 1          | 10.00 |        |             |

Scenario: Transaction - Recorded transactions on sub-accounts are aggregated at upper levels when in different transactions on the same side
	Given I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 500   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	And I record a transaction "TRANS2" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 501   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	And I record a transaction "TRANS3" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 510   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	And I record a transaction "TRANS4" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 511   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	Then the content of the TAccount "50" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 |        |             |
		| 2          | 10.00 |        |             |
	And the content of the TAccount "51" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 3          | 10.00 |        |             |
		| 4          | 10.00 |        |             |
	And the content of the TAccount "5" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 |        |             |
		| 2          | 10.00 |        |             |
		| 3          | 10.00 |        |             |
		| 4          | 10.00 |        |             |

Scenario: Transaction - Recorded transactions on sub-accounts are aggregated at upper levels when in different transactions on different sides
	Given I record a transaction "TRANS1" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 500   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	And I record a transaction "TRANS2" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 10.00        |               |
		|       | 501    |              | 10.00         |
	And I record a transaction "TRANS3" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 510   |        | 10.00        |               |
		|       | 100    |              | 10.00         |
	And I record a transaction "TRANS4" on ledger "MyLedger"
		| Debit | Credit | Amount Debit | Amount Credit |
		| 100   |        | 10.00        |               |
		|       | 511    |              | 10.00         |
	Then the content of the TAccount "50" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 | 10.00  | 2           |
	And the content of the TAccount "51" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 3          | 10.00 | 10.00  | 4           |
	And the content of the TAccount "5" on ledger "MyLedger" is
		| TransDebit | Debit | Credit | TransCredit |
		| 1          | 10.00 | 10.00  | 2           |
		| 3          | 10.00 | 10.00  | 4           |





