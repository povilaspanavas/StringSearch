using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class ClassResult
    {
        private string _name;
        private List<MethodResult> _methodResults = new List<MethodResult>();

        public ClassResult()
        {

        }

        public ClassResult(Type type)
            : this()
        {
            this._name = type.Name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<MethodResult> MethodResults
        {
            get { return _methodResults; }
            set { _methodResults = value; }
        }

        int _failedCount = 0;
        int _successCount = 0;
        int _ignoredCount = 0;

        public int FailedCount
        {
            get { return _failedCount; }
            set { _failedCount = value; }
        }

        public int SuccessCount
        {
            get { return _successCount; }
            set { _successCount = value; }
        }

        public int IgnoredCount
        {
            get { return _ignoredCount; }
            set { _ignoredCount = value; }
        }

        public bool Success
        {
            get { return _failedCount == 0; }
        }
    }
}
