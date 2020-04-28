using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public  class DalFactory
    {
        private static DAL.Idal instance=null;

        //protected DalFactory() { instance = null; }
        public static DAL.Idal getDal()
        {
            if (null == instance)
                instance = new imp_XML_Dal();
            return instance;
        }
    }
}
