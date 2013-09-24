using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringSearch.Tests;
using PUnit.Framework.Tests.MockData;

namespace PUnit.Framework.Tests
{
    [NUnit.Framework.TestFixture]
    class TestRunner
    {
        private Runner runner = new Runner();
        [NUnit.Framework.Test]
        public void Run()
        {
            NUnit.Framework.Assert.IsTrue(runner.Run(typeof(TestFindString).Assembly).Success);
            NUnit.Framework.Assert.IsTrue(0 < runner.Run(typeof(TestFindString).Assembly).SuccessCount);
        }

        [NUnit.Framework.Test]
        public void ExecuteTestFixture()
        {
            var classResult = runner.ExecuteTestFixture(typeof(ClassOneTest));
            NUnit.Framework.Assert.IsTrue(classResult.Success);
            NUnit.Framework.Assert.AreEqual(typeof(ClassOneTest).Name, classResult.Name);
            NUnit.Framework.Assert.AreEqual(1, classResult.MethodResults.Count);
        }

        [NUnit.Framework.Test]
        public void ExecuteTestFixtureNoTestsInClass()
        {
            var classResult = runner.ExecuteTestFixture(typeof(ClassNoTestFixture));
            NUnit.Framework.Assert.IsTrue(classResult.Success);
            NUnit.Framework.Assert.AreEqual(typeof(ClassNoTestFixture).Name, classResult.Name);
            NUnit.Framework.Assert.AreEqual(0, classResult.MethodResults.Count);
        }

        [NUnit.Framework.Test]
        public void ExecuteRealTests()
        {
            List<Type> types = new List<Type>();
            types.Add(typeof(TestFindString));
            var classResults = runner.ExecuteAllTestFixtures(types);
            NUnit.Framework.Assert.AreEqual(1, classResults.Count);
            var classResult = classResults[0];
            NUnit.Framework.Assert.AreEqual(17, classResult.MethodResults.Count);
        }
    }
}
