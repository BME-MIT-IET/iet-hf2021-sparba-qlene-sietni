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


    }
}
