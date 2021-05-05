using NUnit.Framework;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BDDTest.Features
{
	[Binding]
	public sealed class CreatePatternSteps
	{
        RDFVariable x, y, z;
        RDFPattern pattern;
        [Given(@"create subject, predicate, object variables")]
        public void GivenCreateSubjectPredicateObjectVariables()
        {
            x = new RDFVariable("x");
            y = new RDFVariable("y");
            z = new RDFVariable("z");
        }

        [When(@"create a pattern with variables")]
        public void WhenCreateAPatternWithVariables()
        {
            pattern = new RDFPattern(x, y, z);
        }

        [Then(@"pattern should be created")]
        public void ThenPatternShouldBeCreated()
        {
            Assert.IsNotNull(pattern);
            Assert.AreEqual(x, pattern.Subject);
            Assert.AreEqual(y, pattern.Predicate);
            Assert.AreEqual(z, pattern.Object);
        }
    }
}
