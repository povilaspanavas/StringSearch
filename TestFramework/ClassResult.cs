using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class ClassResult
    {
        private bool _success;
        private List<MethodResult> _methodResults;

        public List<MethodResult> MethodResults
        {
            get { return _methodResults; }
            set { _methodResults = value; }
        }

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

    }
}
