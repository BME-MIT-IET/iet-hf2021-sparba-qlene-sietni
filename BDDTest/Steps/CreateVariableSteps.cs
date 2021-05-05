using NUnit.Framework;
using RDFSharp.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RDFSharp.Steps
{
	[Binding]
	public sealed class CreateVariableSteps
	{
        RDFVariable test;
        [Given(@"create a variable")]
        public void GivenCreateAVariable()
        {
            test = new RDFVariable("test");
        }

        [Then(@"variable should be created")]
        public void ThenVariableShouldBeCreated()
        {
            Assert.IsNotNull(test);
            Assert.AreEqual("?TEST", test.VariableName);
        }
    }
}
