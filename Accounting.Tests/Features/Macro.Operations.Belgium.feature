Feature: Macro - Operations - Belgium

Background:
	Given I create a ledger "L" with the properties
		| Property | Value    |
		| Name     | MyLedger |
		| Depth    | 4        |
	And I load t-accounts from file "be_fr_pcmn_full.json" on ledger "L"

Scenario: Macro - Operations - Belgium - Invoice (Buy)
	Given I create a macro "M" with the properties
		| Property | Value |
		| Name     | M     |
	And I update the script of the macro "M" to 
		"""
		def __main():
			
			total = __TOTAL__
			bi = total / ( 1 + __VAT__ )
			vat = total - bi

			t = _ledger.T()
			t.D('2402', bi)
			t.D('411', vat)
			t.C('440', total)

			t2 = _ledger.T()
			t2.D('440', total)
			t2.C('550', total)

		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR" with parameters
		| Name  | Value  |
		| TOTAL | 121.00 |
		| VAT   | 0.21   |
	Then the content of ledger "L" is 
		| Trans | Debit | Credit | Debit Amount | Credit Amount |
		| 1     | 2402  |        | 100.00       |               |
		| 1     | 411   |        | 21.00        |               |
		| 1     |       | 440    |              | 121.00        |
		| 2     | 440   |        | 121.00       |               |
		| 2     |       | 550    |              | 121.00        |

Scenario: Macro - Operations - Belgium - Invoice (Sell)
	Given I create a macro "M" with the properties
		| Property | Value |
		| Name     | M     |
	And I update the script of the macro "M" to 
		"""
		def __main():
			
			ht = __HT__
			vat = ht * __VAT__
			total = ht + vat

			t = _ledger.T()
			t.D('400', total)
			t.C('700', ht)
			t.C('451', vat)

			t2 = _ledger.T()
			t2.D('550', total)
			t2.C('400', total)

		#enddef
		"""
	When I execute the macro "M" on ledger "L" into result "MR" with parameters
		| Name | Value  |
		| HT   | 100.00 |
		| VAT  | 0.21   |
	Then the content of ledger "L" is 
		| Trans | Debit | Credit | Debit Amount | Credit Amount |
		| 1     | 400   |        | 121.00       |               |
		| 1     |       | 451    |              | 21.00         |
		| 1     |       | 700    |              | 100.00        |
		| 2     | 550   |        | 121.00       |               |
		| 2     |       | 400    |              | 121.00        |
