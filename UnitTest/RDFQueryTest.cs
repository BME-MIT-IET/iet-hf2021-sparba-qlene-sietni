using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using RDFSharp.Query;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.IO;
using static RDFSharp.Model.RDFVocabulary;

namespace RDFSharp.UnitTest
{
    [TestClass]
    public class RDFQueryTest
    {
        RDFGraph graph = new RDFGraph();

        [TestMethod]
        public void TestRDFQueryWithTriplePattern() {
            var subject_x = new RDFVariable("x"); 
            var object_y = new RDFVariable("y");
            var name = new RDFVariable("c");

            var filter = new RDFRegexFilter(object_y, new System.Text.RegularExpressions.Regex("7"));

            var dogOf = new RDFResource(RDFVocabulary.DC.BASE_URI + "dogOf");

            var y_dogOf_x = new RDFPattern(object_y, dogOf, subject_x);
            var name_of_dog = new RDFPattern(object_y, RDFVocabulary.FOAF.NAME, name);

            var pg1 = new RDFPatternGroup("PG1").AddPattern(y_dogOf_x).AddFilter(filter);
            pg1.AddPattern(name_of_dog);

            RDFSelectQuery query = new RDFSelectQuery();

            for (int i = 0; i < 10; i++)
            {
                var dog = new RDFResource(RDFVocabulary.RDF.BASE_URI+" "+i+". dog");
                var owner = new RDFResource(RDFVocabulary.RDF.BASE_URI+" "+i+". owner");
                graph.AddTriple(new RDFTriple(dog, dogOf, owner));
                graph.AddTriple(new RDFTriple(dog, RDFVocabulary.FOAF.NAME, new RDFPlainLiteral("kutya "+i)));
            }

            var result = query.AddProjectionVariable(name).AddPatternGroup(pg1).ApplyToGraph(graph);

            var c = result.SelectResults.Rows[0].ItemArray[0].ToString();

            Assert.AreEqual("kutya 7", c);
        }
    }
}
