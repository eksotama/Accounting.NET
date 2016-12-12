Feature: Ledger

#### CREATE

Scenario: Ledger - Create a ledger - NF
	When I create a ledger "L" with the properties
		| Property | Value   |
		| Name     | Ledger1 |
		| Depth    | 3       |
	Then I receive this ok message: "Ledger.Created.Ok"

Scenario Outline: Ledger - Create a ledger - Mandatory properties missing
	When I create a ledger "L" with the properties
		| Property | Value   |
		| Name     | <Name>  |
		| Depth    | 3       |
	Then I receive this error message: "<Message>"

	Examples: 
	| Description | Name | Message           |
	| Name        |      | Ledger.Name.Empty |

Scenario: Ledger - Create a ledger - Depth must be a positive number
	When I create a ledger "L" with the properties
		| Property | Value   |
		| Name     | Ledger1 |
		| Depth    | 0       |
	Then I receive this error message: "Ledger.Depth.MustBeAPositiveNumber"

