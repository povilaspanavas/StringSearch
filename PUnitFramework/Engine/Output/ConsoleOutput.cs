using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Results;

namespace PUnit.Framework.Engine
{
    /// <summary>
    /// Very simple console outputer. Doesn't include details about failing 
    /// for simplicity of this task
    /// </summary>
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
