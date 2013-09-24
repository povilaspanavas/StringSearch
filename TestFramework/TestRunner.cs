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
        public List<ClassResult> Run(Assembly assembly)
        {
            var testTypes = ExtractTestTypes(assembly);
            var testMethods = ExtractTestMethods(testTypes[0]);
            var result = ExecuteTestFixture(testTypes[0]);
            return null;
        }

        public string ExecuteTestFixture(Type type)
        {
            var testMethods = ExtractTestMethods(type);
            if (testMethods.Count == 0)
                return "0";

            ClassResult classResult = new ClassResult(type);
            object typeObject = Activator.CreateInstance(type);
            foreach (MethodInfo method in testMethods)
            {
                 NUnit.Framework.IgnoreAttribute attributeIgnore = AttributeParser.GetAttribute<NUnit.Framework.IgnoreAttribute>(method);
                 if (attributeIgnore != null)
                     continue;
                 NUnit.Framework.ExpectedExceptionAttribute attributeExpectedException = AttributeParser.GetAttribute<NUnit.Framework.ExpectedExceptionAttribute>(method);
                 if (attributeExpectedException != null && attributeExpectedException.ExpectedException != null)
                     classResult.MethodResults.Add(ExecuteMethodExpectedException(typeObject, method, attributeExpectedException.ExpectedException));
                 else
                     classResult.MethodResults.Add(ExecuteMethod(typeObject, method));
    
                 
            }
            return "adf";
        }

        private MethodResult ExecuteMethod(object classObject, MethodInfo method)
        {
            var methodResult = new MethodResult(method);
            try
            {
                method.Invoke(classObject, null);
            }
            catch (Exception ex)
            {
                // TODO remove code duplication
                methodResult.Success = false;
                methodResult.Exception = ex;
                methodResult.FailMessage = "Unexpected exception occured: " + ex.Message;
            }
            return methodResult;
        }

        public MethodResult ExecuteMethodExpectedException(object classObject, MethodInfo method, Type expectedException)
        {
            var methodResult = new MethodResult(method);
            try
            {
                method.Invoke(classObject, null);
            }
            catch (Exception ex)
            {
                if (expectedException.GetType() == ex.GetType())
                    return methodResult;
                else
                    methodResult.Success = false;
                    methodResult.Exception = ex;
                    methodResult.FailMessage = "Unexpected exception occured: " + ex.Message;
            }
            methodResult.Success = false;
            methodResult.FailMessage = "Expected exception didn't occur";
            return methodResult;
        }

        public List<MethodInfo> ExtractTestMethods(Type type)
        {
            var methodsToTest = new List<MethodInfo>();
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo methodInfo in methods)
            {
                NUnit.Framework.TestAttribute attribute =  AttributeParser.GetAttribute<NUnit.Framework.TestAttribute>(methodInfo);
                if (attribute != null)
                    methodsToTest.Add(methodInfo);
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
            var result = testRunner.ExtractTestMethods(typeof(TestFindString));
            NUnit.Framework.Assert.AreEqual(17, result.Count);
            //NUnit.Framework.Assert.AreEqual(typeof(TestFindString).FullName, testTypes[0].FullName);
        }

        [NUnit.Framework.Test]
        public void TestExecuteTestFicture()
        {
            var testRunner = new TestRunner();
            var ghmz = testRunner.ExecuteTestFixture(typeof(TestFindString));
            //NUnit.Framework.Assert.AreEqual(typeof(TestFindString).FullName, testTypes[0].FullName);
        }
    }
}
