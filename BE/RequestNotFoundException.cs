using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public  class RequestNotFoundException : Exception
    {
        private string message = "Request not found !";
        public RequestNotFoundException(string message) : base(message)
        {

        }
        public RequestNotFoundException ()
        {
        
        }
        

    }
}
