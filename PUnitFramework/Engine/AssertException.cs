using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class AssertException : Exception
    {
        private string _errorMessage = string.Empty;

        public AssertException() : this(string.Empty)
        {

        }

        public AssertException(string errorMessage) : base()
        {
            this._errorMessage = errorMessage;
        }
    }
}
