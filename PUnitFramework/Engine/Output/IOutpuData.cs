using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PUnit.Framework.Engine
{
    public interface IOutputData
    {
        void Write(SuiteResult data);
    }
}
