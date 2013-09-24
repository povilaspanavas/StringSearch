using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class MethodResult
    {
        private bool _success;
        private Exception _exception;
        private string _failMessage;

        public string FailMessage
        {
            get { return _failMessage; }
            set { _failMessage = value; }
        }

        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

    }
}
