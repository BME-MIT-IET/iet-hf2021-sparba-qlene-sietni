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
        RDFResource agePred = new RDFResource(RDFVocabulary.RDF.BASE_URI + "age");
        RDFSelectQuery query = new RDFSelectQuery();
        RDFPatternGroup patternGroup;
        RDFSelectQueryResult result;

        [Given(@"create a graph with name and age")]
        public void GivenCreateAGraphWithNameAndAge()
        {
            graph = new RDFGraph();
            RDFResource personResource = new RDFResource(RDFVocabulary.RDF.BASE_URI + "person");
            RDFTypedLiteral ageLiteral = new RDFTypedLiteral("old", RDFModelEnums.RDFDatatypes.XSD_STRING);
            graph.AddTriple(new RDFTriple(personResource, agePred, ageLiteral));
            for (int i = 0; i < 10; i++)
            {
                personResource = new RDFResource(RDFVocabulary.RDF.BASE_URI + "person" + i);
                ageLiteral = new RDFTypedLiteral(new Random().Next(10, 41).ToString(), RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER);
                RDFTriple triple = new RDFTriple(personResource, agePred, ageLiteral);
                graph.AddTriple(triple);
            }
        }

        RDFVariable person;
        RDFVariable age;

        [Given(@"write a select query")]
        public void GivenWriteASelectQuery()
        {
            query = new RDFSelectQuery();
            person = new RDFVariable("person");
            age = new RDFVariable("age");
            patternGroup = new RDFPatternGroup("PatternGroup").AddPattern(new RDFPattern(person, age, age));

        }

        [Given(@"apply a filter so age >= ""(.*)""")]
        public void GivenApplyAFilterSoAge(int p0)
        {
            patternGroup.AddFilter(new RDFComparisonFilter(RDFQueryEnums.RDFComparisonFlavors.GreaterOrEqualThan, age, new RDFTypedLiteral(p0.ToString(), RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER)));
        }

        [Given(@"age must be nonnegative integer")]
        public void GivenAgeMustBeNumeric()
        {
            patternGroup.AddFilter(new RDFDatatypeFilter(age, RDFModelEnums.RDFDatatypes.XSD_NONNEGATIVEINTEGER));
            query.AddPatternGroup(patternGroup).AddProjectionVariable(person).AddProjectionVariable(age);
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
            for (int i = 0; i < result.SelectResultsCount; i++)
            {
                Assert.IsTrue(int.Parse(result.SelectResults.Rows[i].ItemArray[1].ToString().Split("^")[0]) >= 20);
            }
        }

    }
}
