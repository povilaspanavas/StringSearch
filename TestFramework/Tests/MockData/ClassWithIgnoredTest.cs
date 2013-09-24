using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [NUnit.Framework.TestFixture]
    public class ClassWithIgnoredTest
    {
        [NUnit.Framework.Test, NUnit.Framework.Ignore]
        public void IgnoredTest()
        {

        }
    }
}
