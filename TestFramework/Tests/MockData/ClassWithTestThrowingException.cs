using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [NUnit.Framework.TestFixture]
    class ClassWithTestThrowingException
    {
        [NUnit.Framework.Test]
        public void TestThrowsException()
        {
            throw new Exception("I'm a failing test");
        }
    }
}
