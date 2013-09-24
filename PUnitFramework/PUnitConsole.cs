using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PUnit.Framework
{
    class PUnitConsole
    {
        public void RunTests(Assembly assembly)
        {
            SuiteResult testsResult = new Runner().Run(assembly);
            
        }
    }
}
