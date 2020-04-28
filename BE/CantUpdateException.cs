using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CantUpdateException : Exception
    {
        public CantUpdateException(String message) : base(message) { }


    }
}
