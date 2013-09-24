using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StringSearch;
using PUnit.Framework;

namespace StringSearch.Tests
{
    [TestFixture]
    public class TestFindString
    {
        private string _text = "Polly put the kettle on, polly put the kettle on, polly put the kettle on we'll all have tea";

        [Test]
        public void OneMatchOnly()
        {
            Assert.AreEqual(75, new FindString().IndexOf(_text, "we'll"));
        }

        [Test]
        public void SearchWithingVeryLongStringOnly()
        {
            var count = 1000;
            string text = string.Empty;
            for (int i = 0; i < 1000; i++)
                text += this._text;

            Assert.AreEqual(75, new FindString().IndexOf(text, "we'll")); // first occurance
            Assert.AreEqual(count, new FindString().AllIndexesOf(text, "we'll").Count); 
        }


        [Test]
        public void MatchesTheLastString()
        {
            Assert.AreEqual(90, new FindString().IndexOf(_text, "tea"));
        }

        [Test]
        public void MatchesTheLastSimbol()
        {
            string text = "qwertyipopuopuipouioweuroia";
            Assert.AreEqual(text.Length, new FindString().IndexOf(text, "a"));
        }

        [Test]
        public void MatchesTheFirstSimbol()
        {
            string text = "aqwertyipopuopuipouioweuroi";
            Assert.AreEqual(1, new FindString().IndexOf(text, "a"));
        }

        [Test]
        public void SearchForPollyCaseSensitive()
        {
            var indexesList = new FindString(false).AllIndexesOf(_text, "Polly");
            Assert.AreEqual(1, indexesList.Count);
            Assert.AreEqual(1, indexesList[0]);
        }

        [Test]
        public void SearchForTeaCaseSensitive()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString(false).IndexOf(_text, "Tea"));
        }

        [Test]
        public void SearchingForEmptySubtext()
        {
            Assert.AreEqual(0, new FindString().IndexOf(_text, string.Empty));
        }

        [Test]
        public void SearchingWithingEmptyText()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(string.Empty, "sometext"));
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchingForNullSubtext()
        {
            new FindString().IndexOf(_text, null);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchingWithinNullText()
        {
            new FindString().IndexOf(null, _text);
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void SearchingForBothNullParams()
        {
            new FindString().IndexOf(null, null);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SearchingFromOutOfRangeIndex()
        {
            new FindString().IndexOf(_text, "Tea", _text.Length);
        }

        #region Tests from the task's pdf file
        [Test]
        public void SearchForPolly()
        {
            var indexesList = new FindString().AllIndexesOf(_text, "Polly");
            Assert.AreEqual(3, indexesList.Count);
            Assert.AreEqual(1, indexesList[0]);
            Assert.AreEqual(26, indexesList[1]);
            Assert.AreEqual(51, indexesList[2]);
        }

        [Test]
        public void SearchForpolly()
        {
            var indexesList = new FindString().AllIndexesOf(_text, "polly");
            Assert.AreEqual(3, indexesList.Count);
            Assert.AreEqual(1, indexesList[0]);
            Assert.AreEqual(26, indexesList[1]);
            Assert.AreEqual(51, indexesList[2]);
        }

        [Test]
        public void SearchForll()
        {
            var indexesList = new FindString().AllIndexesOf(_text, "ll");
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
            var indexesList = new FindString().AllIndexesOf(_text, "Ll");
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
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(_text, "X"));
        }

        [Test]
        public void NoMatch_Polx()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(_text, "Polx"));
        }

        [Test]
        public void NoMatch_teacups()
        {
            Assert.AreEqual(FindString.InvalidIndex, new FindString().IndexOf(_text, "teacups"));
        }
        #endregion
    }
}
