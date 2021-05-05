Feature: Filter
	Apply filter on query result

@filter
Scenario: List 20 or more years old people
	Given create a graph with name and age
	And write a select query
	And apply a filter so age >= "20"
	And age must be nonnegative integer
	When run this query on the graph
	Then get result with "20" or more years old people

Scenario: List only "old" people
	Given create a graph with name and age
	And write a select query
	And apply a filter to list only "old" people
	When run this query on the graph
	Then get result with "old" people