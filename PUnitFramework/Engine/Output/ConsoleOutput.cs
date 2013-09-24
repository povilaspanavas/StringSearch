using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Engine
{
    public class ConsoleOutput : IOutputData
    {
        public void Write(SuiteResult testResults)
        {
            string resultString = "passed successfully";
            if (testResults.Success == false)
                resultString = "failed";
            Console.WriteLine(string.Format("Tests {0}.", resultString));

            string statistics = "Success {0}, Ignored {1}, Failed {2}";
            Console.WriteLine(string.Format(statistics, testResults.SuccessCount,
                testResults.IgnoredCount, testResults.FailedCount));

        }
    }
}
