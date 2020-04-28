using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
          BL.IBL bL = BlFactory.GetBL();
        bL.setRequests();
            int choice;
            do
            {
                Console.WriteLine("Enter an option\r\n1-->Host\r\n2-->Client\r\n3-->Admin\r\n4-->exit the menu\r\n");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        HostMenu(bL);
                        break;
                    case 2:
                        GuestMenu(bL);
                        break;
                    case 3:
                        AdminMenu(bL);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Option not valid !");
                        break;

                }
            } while (choice != 4);



        }

        private static void HostMenu(BL.IBL bL)
        {
            string hostingUnitName;
            Console.WriteLine("Are you a new host ? (y/n) ");
            Host host = new Host();
            string ans = Console.ReadLine();
            while (ans != "y" && ans!="n")
            {
                Console.WriteLine("Invalid input! ");
                Console.WriteLine("Are you a new host ? (y/n) ");
                ans = Console.ReadLine();
            }
            if (ans == "y") {
                Console.WriteLine("Enter your details");
                Console.WriteLine("Private name");
                host.PrivateName = Console.ReadLine();
                Console.WriteLine("family name");
                host.FamilyName = Console.ReadLine();
                Console.WriteLine("phone number");
                host.PhoneNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("mail address");
                host.MailAddress = Console.ReadLine();
                Console.WriteLine("bank account number");
                host.BankAccountNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter bank account details on this order:");
                Console.WriteLine("Bank number");
                host.BankBranchDetails.BankNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("bank name");
                host.BankBranchDetails.BankName = Console.ReadLine();
                Console.WriteLine("branch number");
                host.BankBranchDetails.BranchNumber = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("branch address");
                host.BankBranchDetails.BranchAddress = Console.ReadLine();
                Console.WriteLine("branch city ");
                host.BankBranchDetails.BranchCity = Console.ReadLine();
            }
            else if (ans == "n")
            {
                Console.Write("Enter your phone number: ");
                host.PhoneNumber = Convert.ToInt32(Console.ReadLine());
                try
                {
                    host = bL.search_Host_Using_Number(host.PhoneNumber);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
           
            Console.WriteLine("Enter an option\r\n1-->Add hosting unit \r\n2-->Update hosting unit \r\n3-->To see" +
                                        " all your units \r\n4-->Erase hosting unit\n5-->Cancel bank clearance\n Any other option-->To go back to menu");
            HostingUnit unit=new HostingUnit();
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                //Adding a new hosting unit
                case 1:
                    Console.WriteLine("Enter hosting unit details: ");
                    Console.WriteLine("Number of adults: ");
                    unit.Adults= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Number of children: ");
                    unit.Children= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Area: 0 for all , 1 for north , 2 for south , 3 for center , 4 for jerusalem ");
                    unit.Area = (Area)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Specify sub area ?  (y/n) ");
                    ans = Console.ReadLine();
                    if (ans == "y")
                    {
                        Console.WriteLine("Enter sub area: ");
                        unit.SubArea = Console.ReadLine();
                    }
                    Console.WriteLine("Hosting unit name: ");
                    unit.HostingUnitName = Console.ReadLine();
                    Console.WriteLine("For each option , enter true if you have it and false if you don`t");
                    Console.WriteLine("Jacuzzi: ");
                    unit.Jacuzzi = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Pool: ");
                    unit.Pool= Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Garden: ");
                    unit.Garden= Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Children attractions: ");
                    unit.ChildrensAttractions= Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Enter the type :0 for hotel , 1 for zimmer , 2 for camping , 3 for house , 4 for appartment:  ");
                    unit.Type= (type)Convert.ToInt32(Console.ReadLine());
                    unit.Owner = host;
                    bL.Add_Hosting_Unit(unit);
                    Console.WriteLine("Added with success !");
                    break;
                case 2:
                    Console.WriteLine("Enter the hosting unit name: ");
                     hostingUnitName = Console.ReadLine();
                    try
                    {
                        unit=bL.search_Hosting_Unit_With_Name(hostingUnitName);
                    }
                    catch(KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    Console.WriteLine("Enter hosting unit details: ");
                    Console.WriteLine("Number of adults: ");
                    unit.Adults = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Number of children: ");
                    unit.Children = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Area: 0 for all , 1 for north , 2 for south , 3 for center , 4 for jerusalem ");
                    unit.Area = (Area)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Specify sub area ?  (y/n) ");
                    ans = Console.ReadLine();
                    if (ans == "y")
                    {
                        Console.WriteLine("Enter sub area: ");
                        unit.SubArea = Console.ReadLine();
                    }
                    Console.WriteLine("Hosting unit name: ");
                    unit.HostingUnitName = Console.ReadLine();
                    Console.WriteLine("For each option , enter true if you have it and false if you don`t");
                    Console.WriteLine("Jacuzzi: ");
                    unit.Jacuzzi = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Pool: ");
                    unit.Pool = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Garden: ");
                    unit.Garden = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Children attractions: ");
                    unit.ChildrensAttractions = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Enter the type :0 for hotel , 1 for zimmer , 2 for camping , 3 for house , 4 for appartment:  ");
                    unit.Type = (type)Convert.ToInt32(Console.ReadLine());
                    unit.Owner = host;
                    try
                    {
                        bL.Update_Hosting_Unit(unit);
                    }
                    catch(KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("Updated with success !");
                    break;
                case 3:
                    print_Hosting_Units_ofHost(host,bL);
                    break;
                case 4:
                    Console.WriteLine("Enter the hosting unit name: ");
                    hostingUnitName = Console.ReadLine();
                    try
                    {
                        unit = bL.search_Hosting_Unit_With_Name(hostingUnitName);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    try
                    {
                        bL.Erase_Hosting_Unit(unit);

                    }
                    catch (Can_tEraseException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    Console.WriteLine("Erased with success!");
                    break;
                case 5:
                    Console.WriteLine("Enter the hosting unit name: ");
                    hostingUnitName = Console.ReadLine();
                    try
                    {
                        unit = bL.search_Hosting_Unit_With_Name(hostingUnitName);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    try
                    {
                        bL.Cancel_Clearance(unit);

                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    catch (Can_tEraseException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    Console.WriteLine("Canceled with success!");
                    break;
                default:
                    return;
            }
        }

        private static void GuestMenu(BL.IBL bL)
        {
            Console.WriteLine("Enter an option\r\n1-->Add guest request \r\n2-->Update guest request \r\nAny other option-->Go back to menu");
            int choice = Convert.ToInt32(Console.ReadLine());
            GuestRequest req = new GuestRequest();
            string ans;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter te request information: ");
                    Console.WriteLine("Id:");
                    req.ID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Private name");
                    req.PrivateName = Console.ReadLine();
                    Console.WriteLine("Family name");
                    req.FamilyName = Console.ReadLine();
                    Console.WriteLine("Mail address");
                    //req.MaiAddress= (MailAddress)Console.ReadLine();
                    Console.WriteLine("Entry date    ( (First 3 letters of the month) (day)(,) (year)   Year:2020)");
                    req.Entry_Date = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Release date   ( (First 3 letters of the month) (day)(,) (year)    Year:2020)");
                    req.ReleaseDate= DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Number of adults: ");
                    req.Adults= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Number of children: ");
                    req.Children= Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Area: 0 for all , 1 for north , 2 for south , 3 for center , 4 for jerusalem ");
                    req.Area = (Area)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Specify sub area ?  (y/n) ");
                    ans = Console.ReadLine();
                    if (ans == "y")
                    {
                        Console.WriteLine("Enter sub area: ");
                        req.SubArea = Console.ReadLine();
                    }
                    Console.WriteLine("Enter the type :0 for hotel , 1 for zimmer , 2 for camping , 3 for house , 4 for appartment:  ");
                    req.Type= (type)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("For each option , enter 0 if you must have it , 1 if it doesn`t matter and 2 if you are not intersted");
                    Console.WriteLine("Jacuzzi: ");
                    req.Jacuzzi = (Interested)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Pool: ");
                    req.Pool=(Interested)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Garden: ");
                    req.Garden=(Interested)Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Children attractions: ");
                    req.ChildrensAttractions=(Interested)Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        bL.Add_Guest_Request(req);
                    }
                    catch(InvalidInputException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    req = bL.search_request(req.ID);
                    List<Order> order_options = bL.send_Orders_To_Request(req);
                    if (order_options.Count==0)
                    { 
                        Console.Write("No available options !");
                        return;
                    }
                    Console.WriteLine("Pick one option by entering the order key");
                    foreach(Order option in order_options)
                    {
                        Console.WriteLine(option.ToString());
                    }
                    int orderKey = Convert.ToInt32(Console.ReadLine());
                    Order order_chosen = new Order();
                    try
                    {
                         order_chosen = bL.search_Order_With_Key(orderKey);
                    }
                    catch(KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    order_chosen.Status = Order_Status.Closed_With_Success;
                    try
                    {
                        bL.Update_Order(order_chosen);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                    Console.WriteLine("We sent you an email with the details of the order");
                    break;
                case 2:
                    Console.WriteLine("What`s your id ? ");
                    req.ID = Convert.ToInt32(Console.ReadLine());
                    if (bL.search_With_Id(req.ID))
                    {
                        Console.WriteLine("Enter te request information: ");
                        Console.WriteLine("Mail address");
                       // req.MaiAddress = Console.ReadLine();
                        Console.WriteLine("Entry date    ( (First 3 letters of the month) (day)(,) (year)  Year:2020)");
                        req.Entry_Date = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Release date   ( (First 3 letters of the month) (day)(,) (year) Year:2020)");
                        req.ReleaseDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Area: 0 for all , 1 for north , 2 for south , 3 for center , 4 for jerusalem ");
                        req.Area = (Area)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Specify sub area ?  (y/n) ");
                        ans = Console.ReadLine();
                        if (ans == "y")
                        {
                            Console.WriteLine("Enter sub area: ");
                            req.SubArea = Console.ReadLine();
                        }
                        Console.WriteLine("Enter the type :0 for hotel , 1 for zimmer , 2 for camping , 3 for house , 4 for appartment:  ");
                        req.Type = (type)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("For each option , enter 0 if you must have it , 1 if it doesn`t matter and 2 if you are not intersted");
                        Console.WriteLine("Jacuzzi: ");
                        req.Jacuzzi = (Interested)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Pool: ");
                        req.Pool = (Interested)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Garden: ");
                        req.Garden = (Interested)Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Children attractions: ");
                        req.ChildrensAttractions = (Interested)Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            bL.Update_Guest_Request(req);
                        }
                        catch(CantUpdateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                        Console.WriteLine("You don`t have requests !");
                    break;
                default:
                    break;
            }
        }

        private static void AdminMenu(BL.IBL bL)
        {
            Console.WriteLine("Enter an option\n1-->See all hosting units\n2-->To see all guest requests" +
                "\n3-->To see all orders\n4-->To see how much money you made\n5-->See all units of specific host\n");
            int option = Convert.ToInt32(Console.ReadLine());
            switch(option)
            {
                case 1:
                    List<HostingUnit> hostList = bL.get_All_units();
                    foreach (HostingUnit unit in hostList)
                        Console.WriteLine(unit.ToString());
                    break;
                case 2:
                    List<GuestRequest> reqList = bL.get_All_Requests();
                    foreach (GuestRequest req in reqList)
                        Console.WriteLine(req.ToString());
                    break;
                case 3:
                    List<Order> orderList = bL.get_All_orders();
                    foreach (Order order in orderList)
                        Console.WriteLine(order.ToString());
                    break;
                case 4:
                    Console.WriteLine(bL.sum_To_Pay_of_Everyone());
                    break;
                case 5:
                    Console.WriteLine("Enter his phone number:");
                    Host host = bL.search_Host_Using_Number(Convert.ToInt32(Console.ReadLine()));
                    print_Hosting_Units_ofHost(host,bL);
                    break;
                default:
                    break;
            }

        }

        private static void print_Hosting_Units_ofHost(Host host, BL.IBL bL)
        {
            List<HostingUnit> temp = bL.get_All_Units_Of_Host(host.PhoneNumber);
            foreach (HostingUnit hostUnit in temp)
                Console.WriteLine(hostUnit.ToString());
        }
    }
}
