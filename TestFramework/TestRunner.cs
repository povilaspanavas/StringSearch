using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using StringSearch.Tests;

namespace PUnit.Framework
{
    public class TestRunner
    {
        public void Run(Assembly assembly)
        {
            var testTypes = ExtractTestTypes(assembly);
            var testMethods = ExtractTestMethods(testTypes);
        }

        public List<MethodInfo> ExtractTestMethods(List<Type> testTypes)
        {
            var methodsToTest = new List<MethodInfo>();
            foreach (Type type in testTypes)
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo methodInfo in methods)
                {
                    NUnit.Framework.TestAttribute attribute =  AttributeParser.GetAttribute<NUnit.Framework.TestAttribute>(methodInfo);
                    if (attribute != null)
                        methodsToTest.Add(methodInfo);
                }
            }
            return methodsToTest;
        }

        public List<Type> ExtractTestTypes(Assembly assembly)
        {
            var testTypes = new List<Type>();
            var types = assembly.GetExportedTypes();
            foreach (Type type in types)
            {
                NUnit.Framework.TestFixtureAttribute attribute = AttributeParser.GetAttribute<NUnit.Framework.TestFixtureAttribute>(type);
                if (attribute != null)
                    testTypes.Add(type);
            }
            return testTypes;
        }
    }
    
    /// <summary>
    /// TODO use special classes only for this test
    /// </summary>
    [NUnit.Framework.TestFixture]
    public class TestTestRunner
    {

        [NUnit.Framework.Test]
        public void TestTypesExtraction()
        {
            var testRunner = new TestRunner();
            var testTypes = testRunner.ExtractTestTypes(typeof(TestFindString).Assembly);
            NUnit.Framework.Assert.AreEqual(1, testTypes.Count);
            NUnit.Framework.Assert.AreEqual(typeof(TestFindString).FullName, testTypes[0].FullName);
        }

        [NUnit.Framework.Test]
        public void TestMethodsExtraction()
        {
            var testRunner = new TestRunner();
            var testTypes = new List<Type>();
            testTypes.Add(typeof(TestFindString));
            var result = testRunner.ExtractTestMethods(testTypes);
            NUnit.Framework.Assert.AreEqual(17, result.Count);
            //NUnit.Framework.Assert.AreEqual(typeof(TestFindString).FullName, testTypes[0].FullName);
        }

    }
}
