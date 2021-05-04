using NUnit.Framework;
using RDFSharp.Model;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using static RDFSharp.Model.RDFVocabulary;

namespace BDDTest.Steps
{
	[Binding]
	public sealed class OrderAndLimitModifierSteps
	{
        RDFGraph graph;
        RDFSelectQuery query;
        RDFSelectQueryResult result;
        [Given(@"create a graph with names and body height")]
        public void GivenCreateAGraphWithNamesAndBodyHeight()
        {
            graph = new RDFGraph();
            for(int i = 0; i < 10; i++)
			{
                RDFResource person = new RDFResource(string.Concat(RDF.BASE_URI, "person" + i));
                RDFTypedLiteral bodyHeight = new RDFTypedLiteral((150 + i * 5).ToString(), RDFModelEnums.RDFDatatypes.XSD_INTEGER);
                graph.AddTriple(new RDFTriple(person, new RDFResource(string.Concat(RDF.BASE_URI, "height")), bodyHeight));
            }
        }

        [Given(@"write a query to get the ""(.*)"" tallest people")]
        public void GivenWriteAQueryToGetTheTallestPeople(int p0)
        {
            query = new RDFSelectQuery();
            var person = new RDFVariable("person");
            var bodyHeight = new RDFVariable("bodyHeight");
            var patternGroup = new RDFPatternGroup("PatternGroup").AddPattern(new RDFPattern(person, new RDFResource(string.Concat(RDF.BASE_URI, "height")), bodyHeight));
            query.AddPatternGroup(patternGroup).AddProjectionVariable(person).AddProjectionVariable(bodyHeight);
            query.AddModifier(new RDFLimitModifier(p0));
            query.AddModifier(new RDFOrderByModifier(bodyHeight, RDFQueryEnums.RDFOrderByFlavors.DESC));
        }

        [When(@"run the query on the graph")]
        public void WhenRunTheQueryOnTheGraph()
        {
            result = query.ApplyToGraph(graph);
        }

        [Then(@"get the ""(.*)"" tallest people")]
        public void ThenGetTheTallestPeople(int p0)
        {
            Assert.IsNotNull(graph);
            Assert.IsNotNull(query);
            Assert.IsNotNull(result);
            Assert.AreEqual(p0, result.SelectResultsCount);
            Assert.AreEqual(195, int.Parse(result.SelectResults.Rows[0].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(190, int.Parse(result.SelectResults.Rows[1].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(185, int.Parse(result.SelectResults.Rows[2].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(180, int.Parse(result.SelectResults.Rows[3].ItemArray[1].ToString().Split("^")[0]));
            Assert.AreEqual(175, int.Parse(result.SelectResults.Rows[4].ItemArray[1].ToString().Split("^")[0]));
        }
    }
}
