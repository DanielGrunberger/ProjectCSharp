using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using static BE.Configuration;

namespace BE
{
    public class Order  
    {
        public Order() { }
        private int hostingUnitKey;
        public int HostingUnitKey
        {
            get { return hostingUnitKey;}
            set { hostingUnitKey = value; }
        }
        private int guestRequestKey;
        public int GuestRequestKey
        {
            get { return guestRequestKey; }
            set { guestRequestKey = value; }
        }
        private int orderKey;
        public int OrderKey
        {
            get { return orderKey; }
            set { orderKey = value; }
        }
        private Order_Status status;
        public Order_Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime createDate = new DateTime();
        public DateTime CreateDate { get => createDate; set => createDate=value ; }
        private DateTime orderDate = new DateTime();
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public  override string ToString()
        {
            string str = "";
            str += "Hosting unit key: " + hostingUnitKey + " Guest Request Key: " + guestRequestKey + " Order key: "
                + orderKey + " Order status: " + status.ToString() + " Create date: " + createDate.ToShortDateString()
                + " Order date: " + OrderDate.ToShortDateString()+"\r\n";
            
            return str;
        }

  
    }
}
