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

}
