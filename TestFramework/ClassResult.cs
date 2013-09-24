using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class ClassResult
    {
        private bool _success;
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

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

    }
}
