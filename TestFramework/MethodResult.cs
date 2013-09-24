using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PUnit.Framework
{
    public class MethodResult
    {
        private bool _success = true;
        private Exception _exception;
        private string _failMessage;
        private string _name;
        
        public MethodResult()
        {

        }

        public MethodResult(MethodInfo methodInfo) : this()
        {
            this._name = methodInfo.Name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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
