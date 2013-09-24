using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Tests.MockData;

namespace PUnit.Framework.Tests
{
    [TestFixture]
    public class TestAttributeParser
    {
        [Test]
        public void NoAttributes()
        {
            Assert.AreEqual(0, AttributeParser.ExtractTestMethods<TestAttribute>(typeof(ClassNoTestFixture)).Count);
        }

        [Test]
        public void OneMethodWithAttribute()
        {
            Assert.AreEqual(1, AttributeParser.ExtractTestMethods<TestAttribute>(typeof(ClassOneTest)).Count);
        }
    }
}
