using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using RDFSharp.Model;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace RDFSharp.BenchmarkDotNet
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class Benchmark
    {
        RDFGraph graph = new RDFGraph();

        [Benchmark]
        public void FillGraphWithSome()
        {
            for(int i = 0; i < 100; i++)
            {
                var r = new RDFResource(RDFVocabulary.RDF.BASE_URI + "resource" + i);
                graph.AddTriple(new RDFTriple(r, RDFVocabulary.RDF.TYPE, new RDFResource(RDFVocabulary.RDF.BASE_URI + "resource")));
                graph.AddTriple(new RDFTriple(r, new RDFResource(RDFVocabulary.RDF.BASE_URI + "name"), new RDFPlainLiteral(i.ToString())));
            }
        }

        [GlobalSetup(Target = nameof(QuerySearchByNamePredicate))]
        public void GlobalSetup()
        {
            FillGraphWithSome();
        }

        [Benchmark]
        [Arguments("2")]
        [Arguments(@"\b2")]
        public void QuerySearchByNamePredicate(string regexString)
        {
            var subj = new RDFVariable("s");
            var pred = new RDFResource(RDFVocabulary.RDF.BASE_URI + "name");
            var obj = new RDFVariable("o");
            var regFilter = new RDFRegexFilter(obj, new Regex(regexString));
            var patternGroup = new RDFPatternGroup("pg")
                .AddPattern(new RDFPattern(subj, pred, obj))
                .AddFilter(regFilter);
            var result = new RDFSelectQuery().AddPatternGroup(patternGroup).AddProjectionVariable(subj).ApplyToGraph(graph);
/*            for (int i = 0; i < result.SelectResults.Rows.Count; i++)
            {
                Console.WriteLine("debug - query result: " + result.SelectResults.Rows[i].ItemArray[0].ToString());
            }
*/        }
    }
}
