using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using RDFSharp.Model;
using RDFSharp.Query;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RDFSharp.BenchmarkDotNet
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Declared)]
    [RankColumn]
    public class Benchmark
    {
        RDFGraph graph1 = new RDFGraph();
        RDFGraph graph2 = new RDFGraph();

        [Benchmark]
        [Arguments(1)]
        public void FillGraphWithSome(int init)
        {
            RDFGraph graph = init switch
            {
                1 => graph1,
                2 => graph2,
                _ => graph1,
            };

            for (int i = 0; i < 100 * init; i += init)
            {
                var r = new RDFResource(RDFVocabulary.RDF.BASE_URI + "resource" + i);
                graph.AddTriple(new RDFTriple(r, RDFVocabulary.RDF.TYPE, new RDFResource(RDFVocabulary.RDF.BASE_URI + "resource")));
                graph.AddTriple(new RDFTriple(r, new RDFResource(RDFVocabulary.RDF.BASE_URI + "name"), new RDFPlainLiteral(i.ToString())));
            }
        }

        [GlobalSetup(Target = nameof(QuerySearchByNamePredicate))]
        public void SetupSearchByName()
        {
            FillGraphWithSome(1);
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
            var result = new RDFSelectQuery().AddPatternGroup(patternGroup).AddProjectionVariable(subj).ApplyToGraph(graph1);
        }

        private string[] stringArray = new string[]
        {
            "13",
            "98",
        };

        public IEnumerable<object> RemoveArgumentSource()
        {
            foreach (string s in stringArray)
            {
                yield return s;
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(RemoveArgumentSource))]
        public void RemoveTripleByNamePredicate(string argString)
        {
            graph1.RemoveTriplesByLiteral(new RDFPlainLiteral(argString));
        }

        [GlobalCleanup(Target = nameof(RemoveTripleByNamePredicate))]
        public void CleanupRemoveByName()
        {
            foreach (string argString in RemoveArgumentSource()) {
                var r = new RDFResource(RDFVocabulary.RDF.BASE_URI + "resource" + argString);
                graph1.AddTriple(new RDFTriple(r, new RDFResource(RDFVocabulary.RDF.BASE_URI + "name"), new RDFPlainLiteral(argString)));
                System.Console.WriteLine("cleanup - restored: " + argString);
            }
        }

        [GlobalSetup(Target = nameof(IntersectWith))]
        public void GlobalSetupIntersectWith()
        {
            FillGraphWithSome(2);
        }

        public IEnumerable<object[]> IntersectArgumentSource()
        {
            yield return new object[]
            {
                graph1,
                graph2,
            };
        }

        [Benchmark]
        [ArgumentsSource(nameof(IntersectArgumentSource))]
        public void IntersectWith(RDFGraph graph1, RDFGraph graph2)
        {
            graph1.IntersectWith(graph2);
        }
    }
}
