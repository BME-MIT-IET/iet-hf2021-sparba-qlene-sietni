using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using RDFSharp.Store;
using System;
using System.Collections.Generic;
using System.IO;
using static RDFSharp.Model.RDFVocabulary;

namespace RDFSharp.UnitTest
{
    [TestClass]
    public class RDFStoreTest
    {
        [TestMethod]
        public void TestRDFQuadrupleCreation()
        {
            try
            {
                RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER)
                    );

                Assert.IsNotNull(mickeymouse);
                Assert.IsNotNull(mickeymouse.Context);
                Assert.IsNotNull(mickeymouse.Predicate);
            }
            catch (Exception e) {
                Assert.IsNotNull(null);
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void TestRDFQuadrupleAdd() {
            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);

            Assert.AreEqual(1, wdStore.QuadruplesCount);

            wdStore.AddQuadruple(donalduck);

            Assert.AreEqual(2, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleDelete() {

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.RemoveQuadruple(mickeymouse);
            wdStore.RemoveQuadruple(donalduck);

            Assert.AreEqual(0, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleSearchBySubject()
        {
            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            var found_by_subject = wdStore.SelectQuadruplesBySubject(new RDFResource("http://www.waltdisney.com/mickey_mouse"));

            Assert.AreEqual(1, found_by_subject.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleSearchByPredicate()
        {
            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            var found_by_predicate_age = wdStore.SelectQuadruplesByPredicate(RDFVocabulary.FOAF.AGE);
            var found_by_predicate_name = wdStore.SelectQuadruplesByPredicate(RDFVocabulary.FOAF.NAME);

            Assert.AreEqual(1, found_by_predicate_age.QuadruplesCount);
            Assert.AreEqual(1, found_by_predicate_name.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleSearchByLiteral()
        {
            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     new RDFContext("http://www.wikipedia.com/"),
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            var found_by_literal = wdStore.SelectQuadruplesByLiteral(new RDFPlainLiteral("Donald Duck", "en"));
            var found_by_literal_wrong = wdStore.SelectQuadruplesByLiteral(new RDFPlainLiteral("Donald Luck"));

            Assert.AreEqual(1, found_by_literal.QuadruplesCount);
            Assert.AreEqual(0, found_by_literal_wrong.QuadruplesCount);
        }
    }
}
