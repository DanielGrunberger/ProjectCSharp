using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        private static int guestRequest_Key = 10000000;
        private static int host_Key = 100000;//6 digits
        private static int hostingUnit_Key  = 1000000;// 7 digits
        private static int order_Key = 20000000;//8 digits
        private static int comission  = 10;
        private static int days_to_expire  = 0;

        public static int GuestRequest_Key
        {
            get { return guestRequest_Key++; }
             set { guestRequest_Key = value; }
        }
        public static int Host_Key
        {
            get { return host_Key++; }
             set { host_Key= value; }
        }
        public static int HostingUnit_Key 
        {
            get { return hostingUnit_Key++; }
             set { hostingUnit_Key= value; }
        }
        public static int Order_Key
        {
            get { return order_Key++; }
            set { order_Key = value; }
        }
        public static int Comission
        {
            get { return comission++; }
             set {comission = value; }
        }
        public static int Days_to_expire
        {
            get { return days_to_expire++; }
            set {days_to_expire = value; }
        }

    }
}
