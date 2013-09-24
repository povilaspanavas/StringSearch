using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Results
{
    public class BasicResult
    {
        protected int _failedCount = 0;
        protected int _successCount = 0;
        protected int _ignoredCount = 0;

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
    }
}
