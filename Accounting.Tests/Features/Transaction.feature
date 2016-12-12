Feature: Transaction

Background:
	Given I create a ledger "L" with the properties
		| Property | Value    |
		| Name     | MyLedger |
		| Depth    | 3        |

	Given I create a TAccount "T100" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 100         |
		| Label    | Account 100 |
	And I create a TAccount "T200" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 200         |
		| Label    | Account 200 |
	And I create a TAccount "T300" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 300         |
		| Label    | Account 300 |
	And I create a TAccount "T500" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 500         |
		| Label    | Account 500 |
	And I create a TAccount "T501" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 501         |
		| Label    | Account 501 |
	And I create a TAccount "T510" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 510         |
		| Label    | Account 510 |
	And I create a TAccount "T511" with the properties
		| Property | Value       |
		| Ledger   | MyLedger    |
		| Type     | Assets      |
		| Number   | 511         |
		| Label    | Account 511 |

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





