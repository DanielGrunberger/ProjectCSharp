using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Can_tEraseException : Exception
    {
        public Can_tEraseException(string message) : base(message) { }
    }
}
