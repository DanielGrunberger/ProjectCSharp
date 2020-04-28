using BE;
using System.Collections.Generic;
using static BE.Configuration;
namespace DS
{
   public  class DataSource
    {
        public static List<HostingUnit> hostingUnitList = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey = HostingUnit_Key,
                Owner = new Host ()
                {
                    PrivateName = "Daniel",
                    FamilyName="Grunberger",
                   BankBranchDetails = new BankBranch()
                    {

                        BankName="Hapoalim",
                        BankNumber=453,
                        BranchAddress="Matityahu 23",
                        BranchCity="Jerusalem",
                        BranchNumber=4922
                    },
                   BankAccountNumber=1244,
                    CollectionClearance =Clearance.Yes,
                    HostKey=Configuration.Host_Key,
                    MailAddress="grunba@gmail.com",
                    PhoneNumber=546665459,
                       Username="Daniel",
                    Password="123",
                    NumOfUnits=1
                   
                },
                ChildrensAttractions=true,
                Pool=true,
                Jacuzzi=true,
                Garden=true,
                Adults=3,
                Children=2,
                Area=Area.Jerusalem,
                Type=type.Hotel,
                HostingUnitName="Beach Paradise"
            },
            new HostingUnit()
            {
                HostingUnitKey=Configuration.HostingUnit_Key,
                Owner=new Host()
                {
                    PrivateName="Yosef",
                    FamilyName="Matityahu",
                    BankBranchDetails = new BankBranch()
                    {
                     
                        BankName="Caspomat",
                        BankNumber=123,
                        BranchAddress="Shlomo Hamelech 10",
                        BranchCity="Bnei Brak",
                        BranchNumber=6574
                    },
                    BankAccountNumber=46352,
                    CollectionClearance=Clearance.Yes,
                    HostKey=Configuration.Host_Key,
                    MailAddress="Yosef18Mat@hotmail.com",
                    PhoneNumber=648442075,
                    Username ="Yosef",
                    Password="456",
                    NumOfUnits=1
                    
                },
                ChildrensAttractions=true,
                Pool=true,
                Jacuzzi=false,
                Garden=false,
                Adults=3,
                Children=2,
                Area=Area.Jerusalem,
                Type=type.Hotel,
                HostingUnitName="Beautiful Zimmer"
            },
            new HostingUnit()
            {
                HostingUnitKey=Configuration.HostingUnit_Key,
                Owner = new Host()
                {
                    PrivateName="Eliahu",
                    FamilyName="Elchanan",
                   BankBranchDetails = new BankBranch()
                    {
                
                        BankName="Makabi",
                        BankNumber=173,
                        BranchAddress="Shlomtzion Malka 19",
                        BranchCity="Tel Aviv",
                        BranchNumber=6636
                    },
                   BankAccountNumber=45262,
                    CollectionClearance=Clearance.Yes,
                    HostKey=Configuration.Host_Key,
                    MailAddress="eliahu.e@hotmail.com",
                    PhoneNumber=66745890,
                       Username="Eliahu",
                    Password="789",
                    NumOfUnits=1
                   
                },
                ChildrensAttractions=true,
                Pool=true,
                Jacuzzi=false,
                Garden=true,
                Adults=2,
                Children=7,
                Area=Area.North,
                Type=type.Zimmer,
                HostingUnitName="Achla Makom"
            }
        };

        public static List<GuestRequest> guestRequestList = new List<GuestRequest>()
        {
            new GuestRequest()
            {
               ID=635362,
               PrivateName="Efraim",
               FamilyName="Zalckenberg",
               GuestRequestKey=Configuration.GuestRequest_Key,
               Area=Area.Jerusalem,
               Adults=2,
               Children=0,
               ChildrensAttractions=Interested.Not_interested,
               Garden =Interested.Could,
               Jacuzzi=Interested.Could,
               Pool=Interested.Obligated,
               MailAddress="Efraim@gmail.com",
               Entry_Date= new System.DateTime(2020,8,12),
               RegistrationDate = new System.DateTime(2020,10,10),
               ReleaseDate = new System.DateTime (2020,10,29),
               Status=Guest_Request_Status.Open,
               Type=type.Hotel
            },
            new GuestRequest()
            {
                ID=45252,
                PrivateName="Avraham",
                FamilyName="Goldsmith",
                GuestRequestKey=Configuration.GuestRequest_Key,
                Area=Area.All,
                Adults=2,
                Children=2,
                ChildrensAttractions=Interested.Obligated,
               Garden =Interested.Could,
               Jacuzzi=Interested.Not_interested,
               Pool=Interested.Obligated,
               MailAddress="avragoldsmith@gmail.com",
               RegistrationDate = new System.DateTime(2020,7,13),
               Entry_Date=new System.DateTime(2020,8,4),
               ReleaseDate = new System.DateTime (2020,8,13),
               Status=Guest_Request_Status.Closed_on_Website,
               Type=type.Hotel
            },
            new GuestRequest()
            {
                ID=516732,
                PrivateName="Naty",
                FamilyName="yudolaswki",
                GuestRequestKey=Configuration.GuestRequest_Key,
                Area=Area.North,
                Adults=2,
                Children=7,
                ChildrensAttractions=Interested.Obligated,
               Garden =Interested.Could,
               Jacuzzi=Interested.Not_interested,
               Pool=Interested.Obligated,
                MailAddress="avragoldsmith@gmail.com",
               RegistrationDate = new System.DateTime(2020,7,15),
               Entry_Date=new System.DateTime(2020,7,20),
               ReleaseDate = new System.DateTime (2020,8,7),
               Status=Guest_Request_Status.Open,
               Type=type.Zimmer
            }
        };
        
       public static List<Order> orderList = new List<Order>()
        {
            new Order()
            {
                CreateDate = new System.DateTime (2020,10,22),
                OrderDate=new System.DateTime(2020,10,22),
               GuestRequestKey=guestRequestList[0].GuestRequestKey,
               HostingUnitKey=hostingUnitList[0].HostingUnitKey,
               OrderKey=Configuration.Order_Key,
               Status=Order_Status.Closed_With_Success
            },
            new Order()
            {
                CreateDate = new System.DateTime (2020,12,22),
               GuestRequestKey=guestRequestList[1].GuestRequestKey,
               HostingUnitKey=hostingUnitList[1].HostingUnitKey,
               OrderKey=Configuration.Order_Key,
               Status=Order_Status.Mail_Sent
            },
            new Order()
            {
                CreateDate = new System.DateTime (2020,12,22),
               GuestRequestKey=guestRequestList[2].GuestRequestKey,
               HostingUnitKey=hostingUnitList[2].HostingUnitKey,
               OrderKey=Configuration.Order_Key,
               Status=Order_Status.Not_Handled
            }
        };

        public static List<Host> hostList = new List<Host>()
        {
            new Host()
            {
                 PrivateName = "Daniel",
                    FamilyName="Grunberger",
                   BankBranchDetails = new BankBranch()
                    {

                        BankName="Hapoalim",
                        BankNumber=453,
                        BranchAddress="Matityahu 23",
                        BranchCity="Jerusalem",
                        BranchNumber=4922
                    },
                   BankAccountNumber=1244,
                    CollectionClearance =Clearance.Yes,
                    HostKey=100000,
                    MailAddress="grunba@gmail.com",
                    PhoneNumber=546665459,
                    Username="Daniel",
                    Password="123",
                    NumOfUnits=1
                   
            },
            new Host()
            {
                 PrivateName="Yosef",
                    FamilyName="Matityahu",
                    BankBranchDetails = new BankBranch()
                    {

                        BankName="Caspomat",
                        BankNumber=123,
                        BranchAddress="Shlomo Hamelech 10",
                        BranchCity="Bnei Brak",
                        BranchNumber=6574
                    },
                    BankAccountNumber=46352,
                    CollectionClearance=Clearance.Yes,
                    HostKey=100001,
                    MailAddress="Yosef18Mat@hotmail.com",
                    PhoneNumber=648442075,
                Username ="Yosef",
                    Password="456",
                    NumOfUnits=1
                   
            },
            new Host()
            {
                 PrivateName="Eliahu",
                    FamilyName="Elchanan",
                   BankBranchDetails = new BankBranch()
                    {

                        BankName="Makabi",
                        BankNumber=173,
                        BranchAddress="Shlomtzion Malka 19",
                        BranchCity="Tel Aviv",
                        BranchNumber=6636
                    },
                   BankAccountNumber=45262,
                    CollectionClearance=Clearance.Yes,
                    HostKey=100002,
                    MailAddress="eliahu.e@hotmail.com",
                    PhoneNumber=66745890,
                    Username="Eliahu",
                    Password="789",
                    NumOfUnits=1

            }
        };
    }
}
