using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   
       public  interface Idal
        {
            //Guest Request
            void Add_Guest_Request(GuestRequest guestRequest);
            void Update_Guest_Request(GuestRequest guestRequest);
            List<GuestRequest> Get_Guest_Request_List();

            //Hosting Unit
            void Add_Hosting_Unit(HostingUnit hostUnit);
            void Erase_Hosting_Unit(HostingUnit hostUnit);
            void Update_Hosting_Unit(HostingUnit hostUnit);
            List<HostingUnit> Get_Hosting_Unit_List();

            //Order
            void Add_Order(Order order);
            void Update_Order(Order order);
            List<Order> Get_Order_List();


        List<Host> Get_Host_List();
        void setRequests();
        List<BankBranch> get_Bank_List();
        void Add_Host(Host host);
        void Update_Host(Host host);
        void Erase_Host(Host host);
    }
}
