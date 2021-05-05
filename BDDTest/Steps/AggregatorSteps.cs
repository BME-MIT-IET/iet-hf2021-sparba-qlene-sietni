using NUnit.Framework;
using RDFSharp.Model;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using static RDFSharp.Model.RDFVocabulary;

namespace BDDTest.Steps
{
    [Binding]
    public class AggregatorSteps
    {
        RDFGraph graph;
        RDFSelectQuery query;
        RDFSelectQueryResult result;
        
        // Create a graph and add triples
        [Given(@"create a graph with students and grades")]
        public void GivenCreateAGraphWithStudentsAndGrades()
        {
            graph = new RDFGraph();
            // student1 with grades: 1, 2, 3, 4, 5
            var studentResource = new RDFResource(RDF.BASE_URI + "student1");
            for (int i = 1; i <= 5; i++)
            {
                var gradeLiteral = new RDFTypedLiteral(i.ToString(), RDFModelEnums.RDFDatatypes.XSD_INTEGER);
                graph.AddTriple(new RDFTriple(studentResource, RDF.VALUE, gradeLiteral));
            }
            // student2 with grades: 2, 3, 4
            studentResource = new RDFResource(RDF.BASE_URI + "student2");
            for (int i = 2; i <= 4; i++)
            {
                var gradeLiteral = new RDFTypedLiteral(i.ToString(), RDFModelEnums.RDFDatatypes.XSD_INTEGER);
                graph.AddTriple(new RDFTriple(studentResource, RDF.VALUE, gradeLiteral));
            }
            // student3 with grades: 3, 4, 5
            studentResource = new RDFResource(RDF.BASE_URI + "student3");
            for (int i = 3; i <= 5; i++)
            {
                var gradeLiteral = new RDFTypedLiteral(i.ToString(), RDFModelEnums.RDFDatatypes.XSD_INTEGER);
                graph.AddTriple(new RDFTriple(studentResource, RDF.VALUE, gradeLiteral));
            }
        }

        // Select query with a pattern
        [Given(@"write a select query to get students with grades")]
        public void GivenWriteASelectQueryToGetStudentsWithGrades()
        {
            query = new RDFSelectQuery();
            var student = new RDFVariable("student");
            var grade = new RDFVariable("grade");
            query.AddPatternGroup(new RDFPatternGroup("PatternGroup").AddPattern(new RDFPattern(student, RDF.VALUE, grade)))
                .AddProjectionVariable(student)
                .AddProjectionVariable(grade);
            
        }
        
        // Apply group by modifier and 5 aggregator so 5 new column will appear: average, count, max, min and sum
        [Given(@"add aggregators to query")]
        public void GivenAddAggregatorsToQuery()
        {
            var student = new RDFVariable("student");
            var grade = new RDFVariable("grade");
            var gm = new RDFGroupByModifier(new List<RDFVariable>() { student })
                .AddAggregator(new RDFAvgAggregator(grade, new RDFVariable("avarage")))
                .AddAggregator(new RDFCountAggregator(grade, new RDFVariable("count")))
                .AddAggregator(new RDFMaxAggregator(grade, new RDFVariable("max"), RDFQueryEnums.RDFMinMaxAggregatorFlavors.Numeric))
                .AddAggregator(new RDFMinAggregator(grade, new RDFVariable("min"), RDFQueryEnums.RDFMinMaxAggregatorFlavors.Numeric))
                .AddAggregator(new RDFSumAggregator(grade, new RDFVariable("sum")));
            query.AddModifier(gm);

        }
        
        [When(@"the query is runned")]
        public void WhenTheQueryIsRunned()
        {
            result = query.ApplyToGraph(graph);
        }
        
        // Check if the results are correct
        [Then(@"we should get the average, count, maximum, minimum and sum of grades of students")]
        public void ThenWeShouldGetTheAverageCountMaximumMinimumAndSumOfGradesOfStudents()
        {
            Assert.IsNotNull(graph);
            Assert.IsNotNull(query);
            Assert.IsNotNull(result);

            Assert.AreEqual(3, int.Parse(result.SelectResults.Rows[0].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(5, int.Parse(result.SelectResults.Rows[0].ItemArray[2].ToString().Split("^")[0]));
            Assert.AreEqual(5, int.Parse(result.SelectResults.Rows[0].ItemArray[3].ToString().Split("^")[0]));
            Assert.AreEqual(1, int.Parse(result.SelectResults.Rows[0].ItemArray[4].ToString().Split("^")[0]));
            Assert.AreEqual(15,int.Parse(result.SelectResults.Rows[0].ItemArray[5].ToString().Split("^")[0]));

            Assert.AreEqual(3, int.Parse(result.SelectResults.Rows[1].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(3, int.Parse(result.SelectResults.Rows[1].ItemArray[2].ToString().Split("^")[0]));
            Assert.AreEqual(4, int.Parse(result.SelectResults.Rows[1].ItemArray[3].ToString().Split("^")[0]));
            Assert.AreEqual(2, int.Parse(result.SelectResults.Rows[1].ItemArray[4].ToString().Split("^")[0]));
            Assert.AreEqual(9, int.Parse(result.SelectResults.Rows[1].ItemArray[5].ToString().Split("^")[0]));

            Assert.AreEqual(4, int.Parse(result.SelectResults.Rows[2].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(3, int.Parse(result.SelectResults.Rows[2].ItemArray[2].ToString().Split("^")[0]));
            Assert.AreEqual(5, int.Parse(result.SelectResults.Rows[2].ItemArray[3].ToString().Split("^")[0]));
            Assert.AreEqual(3, int.Parse(result.SelectResults.Rows[2].ItemArray[4].ToString().Split("^")[0]));
            Assert.AreEqual(12,int.Parse(result.SelectResults.Rows[2].ItemArray[5].ToString().Split("^")[0]));
        }
    }
}
