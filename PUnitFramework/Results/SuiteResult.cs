using System.Collections.Generic;

namespace PUnit.Framework.Results
{
    public class SuiteResult : BasicResult
    {
        List<ClassResult> _successfullTests = new List<ClassResult>();
        List<ClassResult> _failedTests = new List<ClassResult>();

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

        public bool Success
        {
            get { return _failedCount == 0; }
        }
    }
}
