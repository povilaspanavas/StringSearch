using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [TestFixture]
    public class ClassWithIgnoredTest
    {
        [Test, Ignore]
        public void IgnoredTest()
        {

        }
    }
}
