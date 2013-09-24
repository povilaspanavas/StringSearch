using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework
{
    public class SuiteResult
    {
        List<ClassResult> _successfullTests = new List<ClassResult>();
        List<ClassResult> _failedTests = new List<ClassResult>();

        int _failedCount = 0;
        int _successCount = 0;
        int _ignoredCount = 0;

        public List<ClassResult> FailedTests
        {
            get { return _failedTests; }
            set { _failedTests = value; }
        }

        public List<ClassResult> SuccessfullTests
        {
            get { return _successfullTests; }
            set { _successfullTests = value; }
        }

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
