using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using PUnit.Framework.Engine;
using PUnit.Framework.Engine.Output;
using PUnit.Framework.Results;

namespace PUnit.Framework
{
    /// <summary>
    /// This class is able to run tests, like NUnit
    /// </summary>
    public class Runner
    {
        private IOutputData _outputData = null;

        /// <summary>
        /// You can implement IOutputData to write data to any stream you want
        /// and in any format you like
        /// </summary>
        /// <param name="outputData"></param>
        public Runner(IOutputData outputData)
        {
            this._outputData = outputData;
        }

        /// <summary>
        /// This default constructor will not output results anywhere. Use this for testing
        /// </summary>
        public Runner()
        {
            _outputData = new NoOutput();
        }

        /// <summary>
        /// Runs all the tests it can find in provided assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public SuiteResult Run(Assembly assembly)
        {
            var typesWithTests = AttributeParser.ExtractTestTypes<TestFixtureAttribute>(assembly);
            var testsByClassResults = ExecuteAllTestFixtures(typesWithTests);
            var result = MakeSummary(testsByClassResults);
            _outputData.Write(result);
            return result;
        }

        /// <summary>
        /// Collects and summarised data
        /// </summary>
        /// <param name="classResultList"></param>
        /// <returns></returns>
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
                if (classResult.FailedCount == 0) // ignored cases isn't equal to failed ones
                    summary.SuccessfullTests.Add(classResult);
                else
                    summary.FailedTests.Add(classResult);
                summary.FailedCount += classResult.FailedCount;
                summary.IgnoredCount += classResult.IgnoredCount;
                summary.SuccessCount += classResult.SuccessCount;
            }
            return summary;
        }

        /// <summary>
        /// Executes all the tests for the provided types
        /// </summary>
        /// <param name="testTypes"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Executes all methods of the type having TestAttribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ClassResult ExecuteTestFixture(Type type)
        {
            var testMethods = AttributeParser.ExtractTestMethods<TestAttribute>(type);
            if (testMethods.Count == 0)
                return new ClassResult(type);

            ClassResult classResult = new ClassResult(type);
            object typeObject = Activator.CreateInstance(type);
            foreach (MethodInfo method in testMethods)
            {
                IgnoreAttribute attributeIgnore = AttributeParser.GetAttribute<IgnoreAttribute>(method);
                if (attributeIgnore != null)
                {
                    var methodResult = new MethodResult(method);
                    methodResult.Ignored = true;
                    methodResult.Success = false;
                    classResult.MethodResults.Add(methodResult);
                    continue;
                }
                ExpectedExceptionAttribute attributeExpectedException = AttributeParser.GetAttribute<ExpectedExceptionAttribute>(method);
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
                methodResult.Success = false;
                methodResult.Exception = ex;
                methodResult.FailMessage = "Unexpected exception occured: " + ex.Message;
            }
            return methodResult;
        }

        private MethodResult ExecuteMethodExpectedException(object classObject, MethodInfo method, Type expectedException)
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