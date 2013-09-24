using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Tests.MockData
{
    [NUnit.Framework.TestFixture]
    public class ClassWithTestExpectingException
    {
        [NUnit.Framework.Test, NUnit.Framework.ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestThrowsExpectedException()
        {
            throw new ArgumentOutOfRangeException("Out of bounds exception");
        }
    }
}
