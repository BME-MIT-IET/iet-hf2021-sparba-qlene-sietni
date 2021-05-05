Feature: Modifier
	trying some modifiers

@orderandlimitmodifier
Scenario: List the 5 tallest people
	Given create a graph with names and body height
	And write a query to get the "5" tallest people
	When run the query on the graph
	Then get the "5" tallest people