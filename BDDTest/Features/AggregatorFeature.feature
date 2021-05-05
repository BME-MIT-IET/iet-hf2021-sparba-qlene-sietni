Feature: AggregatorFeature
	Using aggregators over group-by partitioned sparql results

@aggregator
Scenario: Calculate average, count, maximum, minimum and sum of grades of students
	Given create a graph with students and grades
	And write a select query to get students with grades
	And add aggregators to query
	When the query is runned
	Then we should get the average, count, maximum, minimum and sum of grades of students