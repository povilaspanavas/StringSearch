using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using StringSearch.Tests;

namespace PUnit.Framework
{
    public class Runner
    {
        public SuiteResult Run(Assembly assembly)
        {
            var typesWithTests = AttributeParser.ExtractTestTypes<NUnit.Framework.TestFixtureAttribute>(assembly);
            var testsByClassResults = ExecuteAllTestFixtures(typesWithTests);
            var result = MakeSummary(testsByClassResults);
            return result;
        }

        public SuiteResult MakeSummary(List<ClassResult> classResultList)
        {
            var summary = new SuiteResult();
            foreach (ClassResult classResult in classResultList)
            {
                if (classResult == null)
                    continue;
                foreach (MethodResult methodResult in classResult.MethodResults)
                {
                    if (methodResult.Ignored)
                        classResult.IgnoredCount++;
                    else if (methodResult.Success == false)
                        classResult.FailedCount++;
                    else
                        classResult.SuccessCount++;
                }
                if (classResult.FailedCount > 0) // ignored cases isn't equal to failed ones
                    summary.SuccessfullTests.Add(classResult);
                else
                    summary.FailedTests.Add(classResult);
                summary.FailedCount += classResult.FailedCount;
                summary.IgnoredCount += classResult.IgnoredCount;
                summary.SuccessCount += classResult.SuccessCount;
            }
            return summary;
        }

        public List<ClassResult> ExecuteAllTestFixtures(List<Type> testTypes)
        {
            List<ClassResult> classResultList = new List<ClassResult>();
            foreach (Type type in testTypes)
            {
                var classResult = ExecuteTestFixture(type);
                if (classResult != null)
                    classResultList.Add(classResult);
            }
            return classResultList;
        }

        public ClassResult ExecuteTestFixture(Type type)
        {
            var testMethods = AttributeParser.ExtractTestMethods<NUnit.Framework.TestAttribute>(type);
            if (testMethods.Count == 0)
                return new ClassResult(type);

            ClassResult classResult = new ClassResult(type);
            object typeObject = Activator.CreateInstance(type);
            foreach (MethodInfo method in testMethods)
            {
                NUnit.Framework.IgnoreAttribute attributeIgnore = AttributeParser.GetAttribute<NUnit.Framework.IgnoreAttribute>(method);
                if (attributeIgnore != null)
                {
                    var methodResult = new MethodResult(method);
                    methodResult.Ignored = true;
                    methodResult.Success = false;
                    classResult.MethodResults.Add(methodResult);
                    continue;
                }
                NUnit.Framework.ExpectedExceptionAttribute attributeExpectedException = AttributeParser.GetAttribute<NUnit.Framework.ExpectedExceptionAttribute>(method);
                if (attributeExpectedException != null && attributeExpectedException.ExpectedException != null)
                    classResult.MethodResults.Add(ExecuteMethodExpectedException(typeObject, method, attributeExpectedException.ExpectedException));
                else
                    classResult.MethodResults.Add(ExecuteMethod(typeObject, method));
            }
            return classResult;
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
                if (expectedException == ex.GetType()
                    || (ex.InnerException != null && expectedException == ex.InnerException.GetType()))
                    return methodResult;
                else
                {
                    methodResult.Success = false;
                    methodResult.Exception = ex;
                    methodResult.FailMessage = "Unexpected exception occured: " + ex.Message;
                    return methodResult;
                }
            }
            methodResult.Success = false;
            methodResult.FailMessage = "Expected exception didn't occur";
            return methodResult;
        }
    }

}