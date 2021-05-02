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
            List<RDFResource> objects = this.BuildExampleObjectList(20);
            RDFGraph graph = this.BuildExampleGraph(objects, 100);

            Assert.AreEqual(100, graph.TriplesCount);
        }

        [TestMethod]
        public void TestClearTriples()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(20);
            RDFGraph graph = this.BuildExampleGraph(objects, 100);

            Assert.AreEqual(100, graph.TriplesCount);

            graph.ClearTriples();
            Assert.AreEqual(0, graph.TriplesCount);
        }

        [TestMethod]
        public void TestRemoveTriplesByObject()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(20);
            RDFGraph graph = this.BuildExampleGraph(objects, 100);

            RDFGraph select = graph.SelectTriplesByObject(objects[0]);
            Assert.AreEqual(5, select.TriplesCount);

            graph = graph.RemoveTriplesByObject(objects[0]);
            Assert.AreEqual(95, graph.TriplesCount);

            graph = graph.SelectTriplesByObject(objects[0]);
            Assert.AreEqual(0, graph.TriplesCount);
        }

        [TestMethod]
        public void TestRDFNTriplesSerialization()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(100);
            RDFGraph graph = this.BuildExampleGraph(objects, 500);


        }

        [TestMethod]
        public void TestSerializeTurtle()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(10);
            RDFGraph graph = this.BuildExampleGraph(objects, 20);

            this.SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.Turtle);
            RDFGraph deserializedGraph = this.DeserializeRDFGraph(RDFModelEnums.RDFFormats.Turtle);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeRdfXml()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(10);
            RDFGraph graph = this.BuildExampleGraph(objects, 20);

            this.SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.RdfXml);
            RDFGraph deserializedGraph = this.DeserializeRDFGraph(RDFModelEnums.RDFFormats.RdfXml);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeNTriples()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(10);
            RDFGraph graph = this.BuildExampleGraph(objects, 20);

            this.SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.NTriples);
            RDFGraph deserializedGraph = this.DeserializeRDFGraph(RDFModelEnums.RDFFormats.NTriples);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        [TestMethod]
        public void TestSerializeTriX()
        {
            List<RDFResource> objects = this.BuildExampleObjectList(10);
            RDFGraph graph = this.BuildExampleGraph(objects, 20);

            this.SerializeRDFGraph(graph, RDFModelEnums.RDFFormats.TriX);
            RDFGraph deserializedGraph = this.DeserializeRDFGraph(RDFModelEnums.RDFFormats.TriX);

            Assert.AreEqual(graph.TriplesCount, deserializedGraph.TriplesCount);
        }

        private List<RDFResource> BuildExampleObjectList(int count)
        {
            List<RDFResource> objects = new List<RDFResource>();

            for (int i = 0; i < count; i++)
            {
                objects.Add(new RDFResource(string.Concat(RDF.BASE_URI, $"object_{i}")));
            }

            return objects;
        }

        private RDFGraph BuildExampleGraph(List<RDFResource> objects, int count)
        {
            RDFGraph graph = new RDFGraph();

            for (int i = 0; i < count; i++)
            {
                RDFResource subject = new RDFResource(string.Concat(RDF.BASE_URI, $"subject_{i}"));
                graph.AddTriple(new RDFTriple(subject, RDF.TYPE, objects[i % objects.Count]));
            }

            return graph;
        }

        private void SerializeRDFGraph(RDFGraph graph, RDFModelEnums.RDFFormats rdfFormat)
        {
            DirectoryInfo directory = Directory.CreateDirectory("output");
            graph.ToFile(rdfFormat, "output/graph.txt");
        }

        private RDFGraph DeserializeRDFGraph(RDFModelEnums.RDFFormats rdfFormat)
        {
            try
            {
                switch (rdfFormat)
                {
                    case RDFModelEnums.RDFFormats.Turtle:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.Turtle, "output/graph.txt");
                    case RDFModelEnums.RDFFormats.RdfXml:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.RdfXml, "output/graph.txt");
                    case RDFModelEnums.RDFFormats.NTriples:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.NTriples, "output/graph.txt");
                    case RDFModelEnums.RDFFormats.TriX:
                        return RDFGraph.FromFile(RDFModelEnums.RDFFormats.TriX, "output/graph.txt");
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