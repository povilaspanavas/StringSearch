using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [TestFixture]
    public class ClassWithTestExpectingException
    {
        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestThrowsExpectedException()
        {
            throw new ArgumentOutOfRangeException("Out of bounds exception");
        }
    }
}
