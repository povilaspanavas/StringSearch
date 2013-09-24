using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StringSearch;

namespace StringSearch.Tests
{
    [TestFixture]
    public class TestFindString
    {
        private string text = "Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea";

        [Test]
        public void OneMatchOnly()
        {
            Assert.AreEqual(74, new FindString("we'll").Search(text));
        }

        [Test]
        public void NoMatch_X()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString("X").Search(text));
        }

        [Test]
        public void NoMatch_Polx()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString("Polx").Search(text));
        }

        [Test]
        public void NoMatch_teacups()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString("teacups").Search(text));
        }
    }
}
