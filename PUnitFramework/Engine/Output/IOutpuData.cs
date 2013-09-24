using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PUnit.Framework.Results;

namespace PUnit.Framework.Engine
{
    /// <summary>
    /// Implement this interface to output your data anywhere from console to xml file
    /// </summary>
    public interface IOutputData
    {
        void Write(SuiteResult data);
    }
}
