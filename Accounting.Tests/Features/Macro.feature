Feature: Macro

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
		| Property    | Value          |
		| Name        | Macro1         |
	And I update the script of the macro "M" to 
		"""
		def __main():
			return <Input>
		#enddef
		"""
	When I execute the macro "M" into result "MR"
	Then the macro result "MR" is "<Output>"

	Examples:
	| Description     | Input       | Output |
	| String          | '20'        | 20     |
	| String (Concat) | '20' + '20' | 2020   |
	| Int             | 20          | 20     |
	| Int (Add)       | 20 + 1      | 21     |
	| Int (Substract) | 20 - 1      | 19     |
	| Int (Multiply)  | 2 * 3       | 6      |
	| Int (Divide)    | 10 / 5      | 2      |

	
