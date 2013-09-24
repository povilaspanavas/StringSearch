using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringSearch.Tests;

namespace PUnit.Framework.Tests
{
    [NUnit.Framework.TestFixture]
    class TestRunner
    {
        [NUnit.Framework.Test]
        public void Test()
        {
            var runner = new Runner();
            NUnit.Framework.Assert.IsTrue(0 < runner.Run(typeof(TestFindString).Assembly).Count);
        }
    }
}
