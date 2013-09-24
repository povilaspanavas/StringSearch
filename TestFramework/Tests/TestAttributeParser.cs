using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Tests.MockData;

namespace PUnit.Framework.Tests
{
    [NUnit.Framework.TestFixture]
    public class TestAttributeParser
    {
        [NUnit.Framework.Test]
        public void NoAttributes()
        {
            NUnit.Framework.Assert.AreEqual(0, AttributeParser.ExtractTestMethods<NUnit.Framework.TestAttribute>(typeof(ClassNoTestFixture)).Count);
        }

        [NUnit.Framework.Test]
        public void OneMethodWithAttribute()
        {
            NUnit.Framework.Assert.AreEqual(1, AttributeParser.ExtractTestMethods<NUnit.Framework.TestAttribute>(typeof(ClassOneTest)).Count);
        }
    }
}
