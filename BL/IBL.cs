using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
   
        public interface IBL
        {
            //Guest Request
            void Add_Guest_Request(GuestRequest guestRequest);
            void Update_Guest_Request(GuestRequest guestRequest);
            List<GuestRequest> Check_Condition_Of_Request(Predicate<GuestRequest> Condition);
            List<GuestRequest> All_Requests_Of_Client(int id);
            double Percentage_Of_People_Intersted(Predicate<GuestRequest> Condition);
            List<GuestRequest>  get_All_Requests();
            IEnumerable<IGrouping<Area, GuestRequest>> Requests_On_This_Area();
            IEnumerable<IGrouping<int, GuestRequest>> Same_Number_Of_People();
            GuestRequest search_request(int key);
            bool search_With_Id(int id);

            //Hosting Unit
            void Add_Hosting_Unit(HostingUnit hostUnit);
            void Erase_Hosting_Unit(HostingUnit hostUnit);
            void Update_Hosting_Unit(HostingUnit hostUnit);
            void Cancel_Clearance(HostingUnit hosUnit);
            List<HostingUnit> Approve_Request(DateTime entryDate, int duration);
            List<HostingUnit> Check_Condition_Of_Unit(Predicate<HostingUnit> Condition);
            //int Number_Of_Clients_To_Host(int PhoneNumber);
            IEnumerable<IGrouping<int, Host>> Units_Of_Host();
            IEnumerable<IGrouping<Area, HostingUnit>> HostingUnits_On_This_Area();
            HostingUnit search_hostingunit(int key);
            List<HostingUnit> get_All_units();
            void print_Annual_Busy_Days_Per_Unit();
            List<HostingUnit> get_All_Units_Of_Host(int PhoneNumber);
            HostingUnit search_Hosting_Unit_With_Name(string name);
            List<HostingUnit> units_of_Host(Host host);

            //Order
            void Add_Order(Order order);
            void Update_Order(Order order);
            List<Order> order_Days_From_Today(int days);
            List<Order> Orders_Sent_To_Request(GuestRequest request);
            List<Order> Orders_Sent_To_Host(Host host);
            List<Order> Orders_Closed_With_Succes_To_Host(HostingUnit unit);
            int Orders_Sent_And_Closed_With_Success(HostingUnit unit);
            Order search_Order_With_Key(int key);
            List<Order> get_All_orders();
            List<Order> send_Orders_To_Request(GuestRequest req); 
            int difference_of_days(params DateTime[] dates);
            void setRequests();
            Host search_Host_Using_Number(int phoneNumber);
            int sum_To_Pay_of_Everyone();

        List<BankBranch> get_Bank_List();

        //Host
        int get_Average_Of_Grades();
        void erase_Host(Host host);
        void Update_Host(Host host);
        void Add_Host(Host host);
        List<Host> GetHosts();
        }
    
}
