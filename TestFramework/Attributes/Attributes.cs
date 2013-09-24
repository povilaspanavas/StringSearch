using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestFixtureAttribute : Attribute
    {
    }

   [AttributeUsage(AttributeTargets.Method)]
    public class TestAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class IgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ExpectedExceptionAttribute : Attribute
    {
        private Type _type;

        public Type ExpectedException
        {
            get { return _type; }
        }

        public ExpectedExceptionAttribute(Type typeOfException)
        {
            _type = typeOfException;
        }
    }
}
