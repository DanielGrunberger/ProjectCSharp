using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  
        public enum type
        {
           Hotel , Zimmer , Camping , House , Appartment
        }
        public enum Area
        {
            All , North , South , Center, Jerusalem
        }
        public enum Order_Status
        {
            Not_Handled , Mail_Sent , Closed_No_Response  , Closed_With_Success
        }
        public enum Guest_Request_Status
        {
                Open , Closed_on_Website , Expired
        }
        public enum Interested
        {
            Obligated , Could , Not_interested
        }
        public enum Clearance//If we could charge
        {
            Yes , No
        }
   
}
