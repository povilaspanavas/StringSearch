using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Tests.MockData;
using PUnit.Framework.Results;

namespace PUnit.Framework.Tests
{
    [TestFixture]
    class TestRunner
    {
        private Runner runner = new Runner();

       

        [Test]
        public void ExecuteTestFixture()
        {
            var classResult = runner.ExecuteTestFixture(typeof(ClassOneTest));
            Assert.IsTrue(classResult.Success);
            Assert.AreEqual(typeof(ClassOneTest).Name, classResult.Name);
            Assert.AreEqual(1, classResult.MethodResults.Count);
        }

        [Test]
        public void ExecuteTestFixtureNoTestsInClass()
        {
            var classResult = runner.ExecuteTestFixture(typeof(ClassNoTestFixture));
            Assert.IsTrue(classResult.Success);
            Assert.AreEqual(typeof(ClassNoTestFixture).Name, classResult.Name);
            Assert.AreEqual(0, classResult.MethodResults.Count);
        }

        [Test]
        public void ExecuteTestWithExpectedException()
        {
            List<Type> types = new List<Type>();
            types.Add(typeof(ClassWithTestExpectingException));
            var classResults = runner.ExecuteAllTestFixtures(types);
            Assert.AreEqual(1, classResults.Count);
            
            var classResult = classResults[0];
            Assert.AreEqual(1, classResult.MethodResults.Count);
            Assert.IsTrue(classResult.Success);
        }

        [Test]
        public void ExecuteTestFixtureWithIgnoredTest()
        {
            var classResult = runner.ExecuteTestFixture(typeof(ClassWithIgnoredTest));
            List<ClassResult> results = new List<ClassResult>();
            results.Add(classResult);
            var suiteResult = runner.MakeSummary(results);
            
            Assert.IsTrue(classResult.Success);
            Assert.AreEqual(typeof(ClassWithIgnoredTest).Name, classResult.Name);
            Assert.AreEqual(1, suiteResult.IgnoredCount);
            Assert.AreEqual(0, suiteResult.FailedCount);
            Assert.AreEqual(0, suiteResult.SuccessCount);

            Assert.AreEqual(1, suiteResult.SuccessfullTests.Count);
            Assert.AreEqual(1, suiteResult.SuccessfullTests[0].MethodResults.Count);
            Assert.IsTrue(suiteResult.SuccessfullTests[0].MethodResults[0].Ignored);
        }
    }
}
