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
            Assert.AreEqual(75, new FindString().IndexOf(text, "we'll"));
        }

        [Test]
        public void MatchesTheLastString()
        {
            Assert.AreEqual(90, new FindString().IndexOf(text, "tea"));
        }

        [Test]
        public void SearchForPolly()
        {
            var indexesList = new FindString().AllIndexesOf(text, "Polly");
            Assert.AreEqual(3, indexesList.Count);
            Assert.AreEqual(1, indexesList[0]);
            Assert.AreEqual(26, indexesList[1]);
            Assert.AreEqual(51, indexesList[2]);
        }

        [Test]
        public void SearchForpolly()
        {
            var indexesList = new FindString().AllIndexesOf(text, "polly");
            Assert.AreEqual(3, indexesList.Count);
            Assert.AreEqual(1, indexesList[0]);
            Assert.AreEqual(26, indexesList[1]);
            Assert.AreEqual(51, indexesList[2]);
        }

        [Test]
        public void SearchForll()
        {
            var indexesList = new FindString().AllIndexesOf(text, "ll");
            Assert.AreEqual(5, indexesList.Count);
            Assert.AreEqual(3, indexesList[0]);
            Assert.AreEqual(28, indexesList[1]);
            Assert.AreEqual(53, indexesList[2]);
            Assert.AreEqual(78, indexesList[3]);
            Assert.AreEqual(82, indexesList[4]);
        }

        [Test]
        public void SearchForLl()
        {
            var indexesList = new FindString().AllIndexesOf(text, "Ll");
            Assert.AreEqual(5, indexesList.Count);
            Assert.AreEqual(3, indexesList[0]);
            Assert.AreEqual(28, indexesList[1]);
            Assert.AreEqual(53, indexesList[2]);
            Assert.AreEqual(78, indexesList[3]);
            Assert.AreEqual(82, indexesList[4]);
        }

        [Test]
        public void NoMatch_X()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(text, "X"));
        }

        [Test]
        public void NoMatch_Polx()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(text, "Polx"));
        }

        [Test]
        public void NoMatch_teacups()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(text, "teacups"));
        }
    }
}
