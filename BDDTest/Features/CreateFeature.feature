Feature: Create
	Try to create a variable, pattern, pattern group

@variable
Scenario: Create variable
	Given create a variable
	Then variable should be created

@pattern
Scenario: Create pattern
	Given create subject, predicate, object variables
	When create a pattern with variables
	Then pattern should be created