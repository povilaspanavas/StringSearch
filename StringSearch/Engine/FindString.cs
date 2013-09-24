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
    /// By default it is case-insensitive
    /// 
    /// Also, it does work like string.IndexOf + 1
    /// </summary>
    public class FindString
    {
        private bool _ignoreCase = true;
        private UnicodeSkipArray _skipArray;

        // Returned index when no match found
        public const int InvalidIndex = -1;

        public FindString()
        {
        }

        public FindString(bool ignoreCase)
        {
            this._ignoreCase = ignoreCase;
        }

        /// <summary>
        /// Initializes this instance to search a new pattern.
        /// </summary>
        /// <param name="subtext">Pattern to search for</param>
        /// <param name="ignoreCase">If true, search is case-insensitive</param>
        public void Initialize(string subtext)
        {

            // Create multi-stage skip table
            _skipArray = new UnicodeSkipArray(subtext.Length);
            // Initialize skip table for this pattern
            if (_ignoreCase)
            {
                for (int i = 0; i < subtext.Length - 1; i++)
                {
                    _skipArray[Char.ToLower(subtext[i])] = (byte)(subtext.Length - i - 1);
                    _skipArray[Char.ToUpper(subtext[i])] = (byte)(subtext.Length - i - 1);
                }
            }
            else
            {
                for (int i = 0; i < subtext.Length - 1; i++)
                    _skipArray[subtext[i]] = (byte)(subtext.Length - i - 1);
            }
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the beginning.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IndexOf(string text, string subtext)
        {
            return IndexOf(text, subtext, 0);
        }

        /// <summary>
        /// Searches for the current pattern within the given text
        /// starting at the specified index.
        /// 
        /// Returns zero if empty subtext provided
        /// </summary>
        /// <param name="text">Text to search</param>
        /// <param name="startIndex">Offset to begin search</param>
        /// <returns></returns>
        public int IndexOf(string text, string subtext, int startIndex)
        {
            if (text == null || subtext == null)
                throw new ArgumentNullException("Both text and subtext must be not null");
            if (string.Empty.Equals(subtext)) // standard string.IndexOf behaviour
                return 0;
            if (string.Empty.Equals(text)) // standard string.IndexOf behaviour
                return InvalidIndex;
            if (startIndex >= text.Length)
                throw new ArgumentOutOfRangeException("Start index must be within text");
            

            Initialize(subtext);

            int i = startIndex;

            // Loop while there's still room for search term
            while (i <= (text.Length - subtext.Length))
            {
                // Look if we have a match at this position
                int j = subtext.Length - 1;
                if (_ignoreCase)
                {
                    while (j >= 0 && Char.ToUpper(subtext[j]) == Char.ToUpper(text[i + j]))
                        j--;
                }
                else
                {
                    while (j >= 0 && subtext[j] == text[i + j])
                        j--;
                }

                if (j < 0)
                {
                    // Match found
                    return i + 1;
                }

                // Advance to next comparision
                i += Math.Max(_skipArray[text[i + j]] - subtext.Length + 1 + j, 1);
            }
            // No match found
            return InvalidIndex;
        }

        /// <summary>
        /// Returns all the indexes of pattern in the text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<int> AllIndexesOf(string text, string subtext)
        {
            var indexesList = new List<int>();
            var index = this.IndexOf(text, subtext);
            while (index != InvalidIndex)
            {
                indexesList.Add(index);
                if (index >= text.Length)
                    break;
                index = this.IndexOf(text, subtext, index - 1 + subtext.Length);
            }
            return indexesList;
        }
    }
}
