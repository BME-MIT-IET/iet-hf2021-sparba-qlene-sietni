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
        public void TestRDFQuadrupleDeleteByContext()
        {
            var wd_context = new RDFContext("http://www.wikipedia.com/");

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     wd_context,
                     new RDFResource("http://www.waltdisney.com/mickey_mouse"),
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     wd_context,
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.RemoveQuadruplesByContext(wd_context);

            Assert.AreEqual(0, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleDeleteBySubject()
        {
            var wd_context = new RDFContext("http://www.wikipedia.com/");
            var subject_m = new RDFResource("http://www.waltdisney.com/mickey_mouse");

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     wd_context,
                     subject_m,
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     wd_context,
                     new RDFResource("http://www.waltdisney.com/donald_duck"),
                     RDFVocabulary.FOAF.NAME,
                     new RDFPlainLiteral("Donald Duck", "en"));

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.RemoveQuadruplesBySubject(subject_m);

            Assert.AreEqual(1, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleDeleteByObject()
        {
            var wd_context = new RDFContext("http://www.wikipedia.com/");
            var subject_m = new RDFResource("http://www.waltdisney.com/mickey_mouse");
            var subject_d = new RDFResource("http://www.waltdisney.com/donald_duck");
            var object_d = new RDFPlainLiteral("Donald Duck", "en");

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     wd_context,
                     subject_m,
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     wd_context,
                     subject_d,
                     RDFVocabulary.FOAF.NAME,
                     object_d);

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.RemoveQuadruplesByLiteral(object_d);

            Assert.AreEqual(1, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleDeleteByPredicate()
        {
            var wd_context = new RDFContext("http://www.wikipedia.com/");
            var subject_m = new RDFResource("http://www.waltdisney.com/mickey_mouse");
            var subject_d = new RDFResource("http://www.waltdisney.com/donald_duck");
            var object_d = new RDFPlainLiteral("Donald Duck", "en");

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     wd_context,
                     subject_m,
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     wd_context,
                     subject_d,
                     RDFVocabulary.FOAF.NAME,
                     object_d);

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.RemoveQuadruplesByPredicate(RDFVocabulary.FOAF.NAME);

            Assert.AreEqual(1, wdStore.QuadruplesCount);
        }

        [TestMethod]
        public void TestRDFQuadrupleClear()
        {
            var wd_context = new RDFContext("http://www.wikipedia.com/");
            var subject_m = new RDFResource("http://www.waltdisney.com/mickey_mouse");
            var subject_d = new RDFResource("http://www.waltdisney.com/donald_duck");
            var object_d = new RDFPlainLiteral("Donald Duck", "en");

            RDFQuadruple mickeymouse
                     = new RDFQuadruple(
                     wd_context,
                     subject_m,
                     RDFVocabulary.FOAF.AGE,
                     new RDFTypedLiteral("85", RDFModelEnums.RDFDatatypes.XSD_INTEGER));

            RDFQuadruple donalduck
                     = new RDFQuadruple(
                     wd_context,
                     subject_d,
                     RDFVocabulary.FOAF.NAME,
                     object_d);

            var wdStore = new RDFMemoryStore();

            wdStore.AddQuadruple(mickeymouse);
            wdStore.AddQuadruple(donalduck);

            wdStore.ClearQuadruples();

            Assert.AreEqual(0, wdStore.QuadruplesCount);
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
