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
    /// </summary>
    public class FindString
    {
        private string _pattern;
        private bool _ignoreCase;
        private UnicodeSkipArray _skipArray;

        // Returned index when no match found
        public const int InvalidIndex = -1;

        public FindString(string pattern)
        {
            Initialize(pattern, false);
        }

        public FindString(string pattern, bool ignoreCase)
        {
            Initialize(pattern, ignoreCase);
        }

        /// <summary>
        /// Initializes this instance to search a new pattern.
        /// </summary>
        /// <param name="pattern">Pattern to search for</param>
        public void Initialize(string pattern)
        {
            Initialize(pattern, false);
        }

        /// <summary>
        /// Initializes this instance to search a new pattern.
        /// </summary>
        /// <param name="pattern">Pattern to search for</param>
        /// <param name="ignoreCase">If true, search is case-insensitive</param>
        public void Initialize(string pattern, bool ignoreCase)
        {
            _pattern = pattern;
            _ignoreCase = ignoreCase;

            // Create multi-stage skip table
            _skipArray = new UnicodeSkipArray(_pattern.Length);
            // Initialize skip table for this pattern
            if (_ignoreCase)
            {
                for (int i = 0; i < _pattern.Length - 1; i++)
                {
                    _skipArray[Char.ToLower(_pattern[i])] = (byte)(_pattern.Length - i - 1);
                    _skipArray[Char.ToUpper(_pattern[i])] = (byte)(_pattern.Length - i - 1);
                }
            }
            else
            {
                for (int i = 0; i < _pattern.Length - 1; i++)
                    _skipArray[_pattern[i]] = (byte)(_pattern.Length - i - 1);
            }
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the beginning.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int Search(string text)
        {
            return Search(text, 0);
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the specified index.
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <param name="startIndex">Offset to begin search</param>
        /// <returns></returns>
        public int Search(string text, int startIndex)
        {
            int i = startIndex;

            // Loop while there's still room for search term
            while (i <= (text.Length - _pattern.Length))
            {
                // Look if we have a match at this position
                int j = _pattern.Length - 1;
                if (_ignoreCase)
                {
                    while (j >= 0 && Char.ToUpper(_pattern[j]) == Char.ToUpper(text[i + j]))
                        j--;
                }
                else
                {
                    while (j >= 0 && _pattern[j] == text[i + j])
                        j--;
                }

                if (j < 0)
                {
                    // Match found
                    return i;
                }

                // Advance to next comparision
                i += Math.Max(_skipArray[text[i + j]] - _pattern.Length + 1 + j, 1);
            }
            // No match found
            return InvalidIndex;
        }
    }
}
