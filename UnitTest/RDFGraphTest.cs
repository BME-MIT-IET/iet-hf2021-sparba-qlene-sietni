using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using static RDFSharp.Model.RDFVocabulary;

namespace RDFSharp.UnitTest
{
    [TestClass]
    public class RDFGraphTest
    {
        [TestMethod]
        public void TestGraphBuilding()
        {
            List<RDFResource> objects = BuildExampleObjectList(20);
            RDFGraph graph = BuildExampleGraph(objects, 100);

            Assert.AreEqual(100, graph.TriplesCount);
        }

        [TestMethod]
        public void TestClearTriples()
        {
            List<RDFResource> objects = BuildExampleObjectList(20);
            RDFGraph graph = BuildExampleGraph(objects, 100);

            Assert.AreEqual(100, graph.TriplesCount);

            graph.ClearTriples();
            Assert.AreEqual(0, graph.TriplesCount);
        }

        [TestMethod]
        public void TestRemoveTriplesByObject()
        {
            List<RDFResource> objects = BuildExampleObjectList(20);
            RDFGraph graph = BuildExampleGraph(objects, 100);

            RDFGraph select = graph.SelectTriplesByObject(objects[0]);
            Assert.AreEqual(5, select.TriplesCount);

            graph = graph.RemoveTriplesByObject(objects[0]);
            Assert.AreEqual(95, graph.TriplesCount);

            graph = graph.SelectTriplesByObject(objects[0]);
            Assert.AreEqual(0, graph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeTurtle()
        {
            List<RDFResource> objects = BuildExampleObjectList(10);
            RDFGraph graph = BuildExampleGraph(objects, 20);

            SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.Turtle);
            RDFGraph deserializedGraph = DeserializeRDFGraph(RDFModelEnums.RDFFormats.Turtle);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeRdfXml()
        {
            List<RDFResource> objects = BuildExampleObjectList(10);
            RDFGraph graph = BuildExampleGraph(objects, 20);

            SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.RdfXml);
            RDFGraph deserializedGraph = DeserializeRDFGraph(RDFModelEnums.RDFFormats.RdfXml);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeNTriples()
        {
            List<RDFResource> objects = BuildExampleObjectList(10);
            RDFGraph graph = BuildExampleGraph(objects, 20);

            SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.NTriples);
            RDFGraph deserializedGraph = DeserializeRDFGraph(RDFModelEnums.RDFFormats.NTriples);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeTriX()
        {
            List<RDFResource> objects = BuildExampleObjectList(10);
            RDFGraph graph = BuildExampleGraph(objects, 20);

            SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.TriX);
            RDFGraph deserializedGraph = DeserializeRDFGraph(RDFModelEnums.RDFFormats.TriX);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        private static List<RDFResource> BuildExampleObjectList(int count)
        {
            List<RDFResource> objects = new List<RDFResource>();

            for (int i = 0; i < count; i++)
            {
                objects.Add(new RDFResource(string.Concat(RDF.BASE_URI, $"object_{i}")));
            }

            return objects;
        }

        private static RDFGraph BuildExampleGraph(List<RDFResource> objects, int count)
        {
            RDFGraph graph = new RDFGraph();

            for (int i = 0; i < count; i++)
            {
                RDFResource subject = new RDFResource(string.Concat(RDF.BASE_URI, $"subject_{i}"));
                graph.AddTriple(new RDFTriple(subject, RDF.TYPE, objects[i % objects.Count]));
            }

            return graph;
        }

        private static void SerializeRDFGraph(RDFGraph graph, RDFModelEnums.RDFFormats rdfFormat)
        {
            DirectoryInfo directory = Directory.CreateDirectory("output");
            string filepath = "output/";

            switch (rdfFormat)
            {
                case RDFModelEnums.RDFFormats.Turtle:
                    filepath += "turtle.txt";
                    break;
                case RDFModelEnums.RDFFormats.RdfXml:
                    filepath += "rdfxml.txt";
                    break;
                case RDFModelEnums.RDFFormats.NTriples:
                    filepath += "ntriples.txt";
                    break;
                case RDFModelEnums.RDFFormats.TriX:
                    filepath += "trix.txt";
                    break;
            }

            graph.ToFile(rdfFormat, filepath);
        }

        private static RDFGraph DeserializeRDFGraph(RDFModelEnums.RDFFormats rdfFormat)
        {
            try
            {
                switch (rdfFormat)
                {
                    case RDFModelEnums.RDFFormats.Turtle:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.Turtle, "output/turtle.txt");
                    case RDFModelEnums.RDFFormats.RdfXml:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.RdfXml, "output/rdfxml.txt");
                    case RDFModelEnums.RDFFormats.NTriples:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.NTriples, "output/ntriples.txt");
                    case RDFModelEnums.RDFFormats.TriX:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.TriX, "output/trix.txt");
                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}