using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BlFactory
    {
        private static BL.IBL instance = null;
        public static BL.IBL GetBL()
        {
            if (null == instance)
                instance = new BL.BL_imp();
            return instance;
        }
    }
}
