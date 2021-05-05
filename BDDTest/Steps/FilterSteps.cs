using NUnit.Framework;
using RDFSharp.Model;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BDDTest.Steps
{
    [Binding]
    public sealed class FilterSteps
    {

        RDFGraph graph;
        RDFResource age = new RDFResource(RDFVocabulary.RDF.BASE_URI + "age");
        RDFSelectQuery query = new RDFSelectQuery();
        RDFPatternGroup patternGroup;
        RDFSelectQueryResult result;

        [Given(@"create a graph with name and age")]
        public void GivenCreateAGraphWithNameAndAge()
        {
            graph = new RDFGraph();
            RDFResource person = new RDFResource(RDFVocabulary.RDF.BASE_URI + "person");
            RDFTypedLiteral ageLiteral = new RDFTypedLiteral("old", RDFModelEnums.RDFDatatypes.XSD_STRING);
            graph.AddTriple(new RDFTriple(person, age, ageLiteral));
            for(int i = 0; i < 10; i++)
            {
                person = new RDFResource(RDFVocabulary.RDF.BASE_URI + "person" + i);
                ageLiteral = new RDFTypedLiteral(new Random().Next(10, 41).ToString(), RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER);
                RDFTriple triple = new RDFTriple(person, age, ageLiteral);
                graph.AddTriple(triple);
            }
        }

        RDFVariable person;
        RDFVariable ageLiteral;
       
        [Given(@"write a select query")]
        public void GivenWriteASelectQuery()
        {
            query = new RDFSelectQuery();
            person = new RDFVariable("person");
            ageLiteral = new RDFVariable("ageLiteral");
            patternGroup = new RDFPatternGroup("PatternGroup").AddPattern(new RDFPattern(person, age, ageLiteral));
            
        }

        [Given(@"apply a filter so age >= ""(.*)""")]
        public void GivenApplyAFilterSoAge(int p0)
        {
            patternGroup.AddFilter(new RDFComparisonFilter(RDFQueryEnums.RDFComparisonFlavors.GreaterOrEqualThan, ageLiteral, new RDFTypedLiteral(p0.ToString(),RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER)));
        }

        [Given(@"age must be nonnegative integer")]
        public void GivenAgeMustBeNumeric()
        {
            patternGroup.AddFilter(new RDFDatatypeFilter(ageLiteral, RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER));
            query.AddPatternGroup(patternGroup).AddProjectionVariable(person).AddProjectionVariable(ageLiteral);
        }

        [When(@"run this query on the graph")]
        public void WhenRunThisQueryOnTheGraph()
        {
            result = query.ApplyToGraph(graph);
        }


        [Then(@"get result with ""(.*)"" or more years old people")]
        public void ThenGetResultWithOrMoreYearsOldPeople(int p0)
        {
            Assert.IsNotNull(graph);
            Assert.IsNotNull(query);
            Assert.IsNotNull(result);
            for (int i = 0; i < result.SelectResultsCount; i++) {
                Assert.IsTrue(int.Parse(result.SelectResults.Rows[i].ItemArray[1].ToString().Split("^")[0]) >= 20);
            }
        }

    }
}
