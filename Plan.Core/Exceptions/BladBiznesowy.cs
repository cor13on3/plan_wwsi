using System;
using System.Collections.Generic;
using System.Text;

namespace Plan.Core.Exceptions
{
    public class BladBiznesowy : Exception
    {
        public BladBiznesowy(string tresc) : base(tresc)
        {
        }
    }
}
