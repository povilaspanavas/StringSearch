using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Results;

namespace PUnit.Framework.Engine.Output
{
    /// <summary>
    /// Use this for tests, does nothing
    /// </summary>
    public class NoOutput : IOutputData
    {
        public void Write(SuiteResult result)
        {
        }
    }
}
