using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringSearch
{
    /// <summary>
    /// Implements Boyer-Moore search algorithm
    /// http://www.blackbeltcoder.com/Articles/algorithms/fast-text-search-with-boyer-moore
    /// 
    /// The licence allows to use this code in any way.
    /// 
    /// By default is case-insensitive
    /// 
    /// Also, it does work like string.IndexOf + 1
    /// </summary>
    public class FindString
    {
        private string _subtext;
        private bool _ignoreCase = true;
        private UnicodeSkipArray _skipArray;

        // Returned index when no match found
        public const int InvalidIndex = -1;

        public FindString(string subtext)
        {
            Initialize(subtext, true);
        }

        public FindString(string subtext, bool ignoreCase)
        {
            Initialize(subtext, ignoreCase);
        }

        /// <summary>
        /// Initializes this instance to search a new pattern.
        /// </summary>
        /// <param name="subtext">Pattern to search for</param>
        public void Initialize(string subtext)
        {
            Initialize(subtext, true);
        }

        /// <summary>
        /// Initializes this instance to search a new pattern.
        /// </summary>
        /// <param name="subtext">Pattern to search for</param>
        /// <param name="ignoreCase">If true, search is case-insensitive</param>
        public void Initialize(string subtext, bool ignoreCase)
        {
            _subtext = subtext;
            _ignoreCase = ignoreCase;

            // Create multi-stage skip table
            _skipArray = new UnicodeSkipArray(_subtext.Length);
            // Initialize skip table for this pattern
            if (_ignoreCase)
            {
                for (int i = 0; i < _subtext.Length - 1; i++)
                {
                    _skipArray[Char.ToLower(_subtext[i])] = (byte)(_subtext.Length - i - 1);
                    _skipArray[Char.ToUpper(_subtext[i])] = (byte)(_subtext.Length - i - 1);
                }
            }
            else
            {
                for (int i = 0; i < _subtext.Length - 1; i++)
                    _skipArray[_subtext[i]] = (byte)(_subtext.Length - i - 1);
            }
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the beginning.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IndexOf(string text)
        {
            return IndexOf(text, 0);
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the specified index.
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <param name="startIndex">Offset to begin search</param>
        /// <returns></returns>
        public int IndexOf(string text, int startIndex)
        {
            int i = startIndex;

            // Loop while there's still room for search term
            while (i <= (text.Length - _subtext.Length))
            {
                // Look if we have a match at this position
                int j = _subtext.Length - 1;
                if (_ignoreCase)
                {
                    while (j >= 0 && Char.ToUpper(_subtext[j]) == Char.ToUpper(text[i + j]))
                        j--;
                }
                else
                {
                    while (j >= 0 && _subtext[j] == text[i + j])
                        j--;
                }

                if (j < 0)
                {
                    // Match found
                    return i + 1;
                }

                // Advance to next comparision
                i += Math.Max(_skipArray[text[i + j]] - _subtext.Length + 1 + j, 1);
            }
            // No match found
            return InvalidIndex;
        }

        /// <summary>
        /// Returns all the indexes of pattern in the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<int> AllIndexesOf(string text)
        {
            var indexesList = new List<int>();
            var index = this.IndexOf(text);
            while (index != InvalidIndex)
            {
                indexesList.Add(index);
                if (index >= text.Length)
                    break;
                index = this.IndexOf(text, index - 1 + _subtext.Length);
            }
            return indexesList;
        }
    }
}
