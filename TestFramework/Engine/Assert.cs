using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class Assert
    {
        private const string AssertFailed = "Assert failed.";
        private const string StandardFormat = "{0} Expected {1}, but was {2}.";

        public static void AreEqual(int expected, int actual)
        {
            if (expected.Equals(actual) == false)
                throw new AssertException(string.Format(StandardFormat, 
                    Assert.AssertFailed, expected, actual));
        }

        public static void IsTrue(bool val)
        {
            if (val == false)
                throw new AssertException(string.Format(StandardFormat,  
                    Assert.AssertFailed, true, val));
        }
    }
}
