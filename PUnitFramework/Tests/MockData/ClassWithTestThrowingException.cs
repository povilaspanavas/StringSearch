using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [TestFixture]
    public class ClassWithTestThrowingException
    {
        [Test]
        public void TestThrowsException()
        {
            throw new Exception("I'm a failing test");
        }
    }
}
