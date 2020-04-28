using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;
using DAL;


namespace BL
{
    public class BL_imp : IBL
    {
        DAL.Idal Dal = DalFactory.getDal();


        #region Guest Request
        /// <summary>
        /// Add guest request
        /// </summary>
        /// <param name="guestRequest"></param>
        public void Add_Guest_Request(GuestRequest guestRequest)
        {
           if (guestRequest.Entry_Date >= guestRequest.ReleaseDate)
                throw new InvalidInputException("The dates are not valid !  From BL");

            if (guestRequest.Adults == 0 && guestRequest.Children == 0)
                throw new InvalidInputException("Invalid number of people ! From BL");
            guestRequest.Status = Guest_Request_Status.Open;
            Dal.Add_Guest_Request(guestRequest.Clone());
           
     
                
        }
        /// <summary>
        /// Updates a request
        /// </summary>
        /// <param name="guestRequest"></param>
        public void Update_Guest_Request(GuestRequest guestRequest)
        {

            List<GuestRequest> list = Dal.Get_Guest_Request_List();

            var request = (from gr in list
                          where gr.ID == guestRequest.ID && (gr.Status == Guest_Request_Status.Closed_on_Website || gr.Status == Guest_Request_Status.Expired)
                          select gr).FirstOrDefault();
           
    
            if (request!=null)
                throw new CantUpdateException("The request is already closed!  From BL");
            try
            {
                guestRequest.GuestRequestKey = list.Find(o => o.ID == guestRequest.ID).GuestRequestKey;
                Dal.Update_Guest_Request(guestRequest.Clone());
            }
            catch (RequestNotFoundException ex)
            {
                throw new RequestNotFoundException(ex.Message);
            }
            return;
            
     
        }
        /// <summary>
        /// Returns all guest requests that match a certain condition
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public List<GuestRequest> Check_Condition_Of_Request(Predicate<GuestRequest> Condition)
        {
            List<GuestRequest> requests = Dal.Get_Guest_Request_List();
            IEnumerable<GuestRequest> result = from req in requests
                                               where Condition(req)
                                               select req;
            return result.ToList<GuestRequest>();
        }
        /// <summary>
        /// Return all requests made by a client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<GuestRequest> All_Requests_Of_Client(int id)
        {
            List<GuestRequest> guestRequests = Dal.Get_Guest_Request_List();
            IEnumerable<GuestRequest> result = from req in guestRequests
                         where req.ID == id
                         select req;
            return result.ToList<GuestRequest>();
        }
        /// <summary>
        /// Returns pecentage of requests intersted on certain condition
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public double Percentage_Of_People_Intersted(Predicate<GuestRequest> Condition)
        {
            int num_of_requests_intersted = 0;
            List<GuestRequest> requests = Dal.Get_Guest_Request_List();
            int total_num_of_requests = requests.Count;
            foreach (var req in requests)
            {
                if (Condition(req))
                    num_of_requests_intersted++;
            }
            return (double)((num_of_requests_intersted / total_num_of_requests) * 100);
        }
        /// <summary>
        /// Print all requests in alphabetical order of private name
        /// </summary>
        public List<GuestRequest> get_All_Requests()
        {
            List<GuestRequest> requests = Dal.Get_Guest_Request_List();
            List<GuestRequest> sortedList = requests.OrderBy(o => o.GuestRequestKey).ToList();
            return sortedList;
        }
        /// <summary>
        ///Groups all requests on the same area
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, GuestRequest>> Requests_On_This_Area()
        {
            List<GuestRequest> requests = Dal.Get_Guest_Request_List();
            var group_by_area =
                from req in requests
                orderby req.Area
                group req by req.Area into newGroup
                select newGroup;
            return group_by_area;
        }
        /// <summary>
        /// Groups all requests with the same ammount of people
        /// </summary>
        /// <returns></returns>
        public  IEnumerable<IGrouping<int, GuestRequest>> Same_Number_Of_People()
        {
            List<GuestRequest> requests = Dal.Get_Guest_Request_List();
            var group_by_area =
                from req in requests
                group req by req.Adults+req.Children into newGroup
                select newGroup;
            return group_by_area;
        }
        /// <summary>
        /// search for a specific request
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GuestRequest search_request(int key)
        {
            List<GuestRequest> reqList = Dal.Get_Guest_Request_List();
            GuestRequest req= reqList.Find(o => o.ID == key);

            if(req==null)
                throw new KeyNotFoundException("The request does not exist!");

            return req;
        }
        /// <summary>
        /// Returns if this client has requests
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool search_With_Id(int id)
        {
            List<GuestRequest> list = Dal.Get_Guest_Request_List();
            GuestRequest req = list.Find(o => o.ID == id);
            if (req != null)
                return true;
            return false;
        }
        #endregion

        #region Hosting Unit
        /// <summary>
        /// Adds a new hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Add_Hosting_Unit(HostingUnit hostUnit)
        {
            
                Dal.Add_Hosting_Unit(hostUnit.Clone());
        }
        /// <summary>
        /// Erases a hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Erase_Hosting_Unit(HostingUnit hostUnit)
        {
            List<Order> ordersList = Dal.Get_Order_List();
            foreach (var order in ordersList)
            {
                if (order.HostingUnitKey == hostUnit.HostingUnitKey)
                {
                    if (order.Status == Order_Status.Mail_Sent)
                        throw new Can_tEraseException("Can't erase the hosting unit.There is an order connected to it ! From BL");
                }
            }
               
    
                    try
                    {
                        Dal.Erase_Hosting_Unit(hostUnit.Clone());

                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message);
                    }
                
       
            
            return;
        }
        /// <summary>
        /// Updates a hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Update_Hosting_Unit(HostingUnit hostUnit)
        {
            try
            {
                Dal.Update_Hosting_Unit(hostUnit.Clone());
            }
            catch(KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
           
        }
        /// <summary>
        /// Cancelling bank clearance
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Cancel_Clearance(HostingUnit hostUnit)
        {
            List<HostingUnit> hostsList = Dal.Get_Hosting_Unit_List();
            List<Order> ordersList = Dal.Get_Order_List();
            foreach (var unit in hostsList)
            {
                if (unit.HostingUnitKey == hostUnit.HostingUnitKey)//We found the hosting unit
                {
                    foreach (var order in ordersList)//Checking if there is an open order connected to it
                    {
                        if (order.HostingUnitKey == hostUnit.HostingUnitKey)
                        {
                            if (order.Status == Order_Status.Not_Handled)
                                throw new Can_tEraseException("Can't cancel bank clearance.There is an order connected to it ! From BL");
                        }
                    }
                    //Updating the host (cancelling the clearance)
                    try
                    {
                        unit.Owner.CollectionClearance = Clearance.No;
                        Dal.Update_Hosting_Unit(unit.Clone());
                    }
                    catch(KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message);
                    }
                    return;
                }
            }
        }
        /// <summary>
        ///Returns all hosting units that can take this request
        /// </summary>
        /// <param name="entryDate"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public List<HostingUnit> Approve_Request(DateTime entryDate, int duration)
        {
            List<HostingUnit> hostsList = Dal.Get_Hosting_Unit_List();
            var options = from unit in hostsList
                          where unit.ApproveRequest(entryDate, duration) == true
                          select unit;
            return options.ToList<HostingUnit>();
        }
        /// <summary>
        /// Returns all units that have a certain condition
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public List<HostingUnit> Check_Condition_Of_Unit(Predicate<HostingUnit> Condition)
        {
            List<HostingUnit> requests = Dal.Get_Hosting_Unit_List();
            IEnumerable<HostingUnit> result = from req in requests
                                               where Condition(req)
                                               select req;
            return result.ToList<HostingUnit>();
        }
        /// <summary>
        /// Groups hosting units by host
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Host>> Units_Of_Host()
        {
            List<HostingUnit> hostsList = Dal.Get_Hosting_Unit_List();
            var group_by_area =
                from hostingUnit in hostsList
                group hostingUnit.Owner by hostingUnit.Owner.NumOfUnits into newGroup
                select newGroup;
            return group_by_area;
        }
        public List<HostingUnit> units_of_Host(Host host)
        {
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            List<HostingUnit> units_of_host = new List<HostingUnit>();
            foreach(HostingUnit unit in list)
            {
                if (unit.Owner.HostKey == host.HostKey)
                    units_of_host.Add(unit);
            }
            return units_of_host;
        }
        /// <summary>
        /// Groyps by hosting units on thre same area
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, HostingUnit>> HostingUnits_On_This_Area()
        {
            List<HostingUnit> hostsList = Dal.Get_Hosting_Unit_List();
            var group_by_area =
                from unit in hostsList
                orderby unit.Area
                group unit by unit.Area into newGroup
                select newGroup;
            return group_by_area;
        }
        /// <summary>
        /// Searches for a hosting unit based on its key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public HostingUnit search_hostingunit(int key)
        {
            HostingUnit unit = Dal.Get_Hosting_Unit_List().Find(o => o.HostingUnitKey == key);
            if (unit != null)
                return unit;
            throw new KeyNotFoundException();
        }
        /// <summary>
        /// Gets all units in key order
        /// </summary>
        public List<HostingUnit> get_All_units()
        {
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            List<HostingUnit> sortedList = list.OrderBy(o => o.HostingUnitKey).ToList();
            return sortedList;
        }
        /// <summary>
        /// Gets all units of host in key order
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <returns></returns>
        public List<HostingUnit> get_All_Units_Of_Host(int PhoneNumber)
        {
            List<HostingUnit> myList = (from hostUnit in Dal.Get_Hosting_Unit_List()
                         where hostUnit.Owner.PhoneNumber == PhoneNumber
                         select hostUnit).ToList<HostingUnit>();
            return myList.OrderBy(o=>o.HostingUnitKey).ToList();

        }
        /// <summary>
        /// Search for a hosting using the hosting unit name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HostingUnit search_Hosting_Unit_With_Name(string name)
        {
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            foreach ( HostingUnit unit in list)
            {
                if (unit.HostingUnitName == name)
                    return unit;
            }
            throw new KeyNotFoundException("The unit does not exist ! From BL");
        }

        public  void print_Annual_Busy_Days_Per_Unit()
        {
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            foreach(HostingUnit unit in list)
            {
                Console.WriteLine("Hosting unit: " + unit.HostingUnitName + " Days taken on the year: " + unit.GetAnnualBusyDays() + "\r\n");
            }
        }
        #endregion

        #region Order
        /// <summary>
        /// add order to database
        /// </summary>
        /// <param name="order"></param>
        public void Add_Order(Order order)
        {
            //Getting the hosting unit of the order
            HostingUnit unit = Dal.Get_Hosting_Unit_List().Find(o => o.HostingUnitKey == order.HostingUnitKey);

            //Getting the guest request of the order
            GuestRequest req = Dal.Get_Guest_Request_List().Find(o => o.GuestRequestKey == order.GuestRequestKey);
 
            //Creating order
            if (unit.ApproveRequest(req))//If this hosting unit can take this guest request
            {
                order.OrderDate = DateTime.Today;
                try
                {
                    Dal.Add_Order(order);
                }
                catch (KeyNotFoundException ex)
                {
                    throw new KeyNotFoundException(ex.Message);
                }
            }
            else
                throw new InvalidInputException("This unit cannot take this request !");

        }
        /// <summary>
        /// Return orderds that the date from sending email/creating of the order is bigger than the number that the funciton got
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<Order> order_Days_From_Today(int days)
        {
            List<Order> list = Dal.Get_Order_List();
            var result = from order in list
                         where (difference_of_days(order.OrderDate) >= days || difference_of_days(order.CreateDate) >= days)
                         select order;
            return (List<Order>)result;

        }
        /// <summary>
        /// Updating order (status)
        /// </summary>
        /// <param name="order"></param>
        ///
        public void Update_Order(Order order)
        {
            //If the order was closed , we can't change it anymore
            Order acceptedOrder = Dal.Get_Order_List().Find(o => o.OrderKey == order.OrderKey);
          
            
            if ( acceptedOrder.Status == Order_Status.Closed_No_Response|| acceptedOrder.Status==Order_Status.Closed_With_Success)
                    throw new CantUpdateException("The order was already closed ! From BL");
                
            
            HostingUnit unit = new HostingUnit();
            try
            {
                unit= search_hostingunit(order.HostingUnitKey);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            GuestRequest req = new GuestRequest();
            try
            {
                req = Dal.Get_Guest_Request_List().Find(o => o.GuestRequestKey == order.GuestRequestKey);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            if (order.Status==Order_Status.Mail_Sent)
            {
                //Sending mail
                MailMessage mail = new MailMessage();
                SmtpClient smtp;
                try
                {
                    Thread thread = new Thread(()=>MailSender(order, acceptedOrder, unit, req));
                    thread.Start();
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return;
            }
            //If we closed the bussines, we will charge him for comission (10 NIS X duration)
            if (order.Status == Order_Status.Closed_With_Success )
            {
                //Checing that he signed
                if (unit.Owner.CollectionClearance == Clearance.Yes)
                {
                    //Finding the duration
                    int duration = (req.ReleaseDate - req.Entry_Date).Days;
                    //Sum to pay
                    unit.Sum_to_pay = duration * Configuration.Comission;
                    //Updating the guest request
                    unit.setRequest(req);
                    Dal.Update_Hosting_Unit(unit);
                    //Updating guest request status
                    req.Status = Guest_Request_Status.Closed_on_Website;
                    Dal.Update_Guest_Request(req);

                    //Updating status(Mail Sent)
                    acceptedOrder.Status = Order_Status.Closed_With_Success;
                    acceptedOrder.OrderDate = DateTime.Today;
                    try
                    {
                        Dal.Update_Order(acceptedOrder.Clone());
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message);
                    }
                    //After the client accepts one of the orders , we close all the other orders connected to him
                    Order tempOrder = new Order();
                    foreach(Order o in Dal.Get_Order_List())
                    {
                        if (o.GuestRequestKey==req.GuestRequestKey && o.OrderKey!=order.OrderKey)
                        {
                            o.Status = Order_Status.Closed_No_Response;
                            tempOrder = o;
                            Dal.Update_Order(tempOrder.Clone());
                        }
                    }
                    return;
                }
                else//If he didnt sign
                {
                    throw new CantUpdateException("Couldn`t send e-mail , client didn`t sign for collection from bank account");
                }
            }
        }

        private MailMessage MailSender(Order order, Order acceptedOrder, HostingUnit unit, GuestRequest req)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(get_All_Requests().Find(X => X.GuestRequestKey == order.GuestRequestKey).MailAddress.ToString());
            mail.From = new MailAddress(unit.Owner.MailAddress);
            mail.Subject = "Your order in Vacations Paradise";
            mail.Body = "Hello and welcome to Vacations Paradise!\nHere are the details of your order:\n" + unit.ToString() + "Order create date: " + order.CreateDate + "\n";
            mail.IsBodyHtml = true;
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("vacationsparadise18@gmail.com", "yces24ou"),
                EnableSsl = true
            };
            try
            {

                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                acceptedOrder.Status = Order_Status.Mail_Sent;
                Dal.Update_Order(acceptedOrder.Clone());

                //After the client accepts one of the orders , we close all the other orders connected to him
                Order tempOrder = new Order();
                foreach (Order o in Dal.Get_Order_List())
                {
                    if (o.GuestRequestKey == req.GuestRequestKey && o.OrderKey != acceptedOrder.OrderKey)
                    {
                        o.Status = Order_Status.Closed_No_Response;
                        tempOrder = o;
                        Dal.Update_Order(tempOrder.Clone());
                    }
                }
                if (unit.Owner.CollectionClearance == Clearance.Yes)
                {
                    //Finding the duration
                    int duration = (req.ReleaseDate - req.Entry_Date).Days;
                    //Sum to pay
                    unit.Sum_to_pay = duration * Configuration.Comission;
                    //Updating the guest request
                    unit.setRequest(req);
                    Dal.Update_Hosting_Unit(unit);
                    //Updating guest request status
                    req.Status = Guest_Request_Status.Closed_on_Website;
                    Dal.Update_Guest_Request(req);
                    acceptedOrder.OrderDate = DateTime.Today;
                    try
                    {
                        Dal.Update_Order(acceptedOrder.Clone());
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message);
                    }

                }
            }

            return mail;
        }

        private void SendMail(Order order, HostingUnit unit, out MailMessage mail, out SmtpClient smtp)
        {
            mail = new MailMessage();
            mail.To.Add(get_All_Requests().Find(X => X.GuestRequestKey == order.GuestRequestKey).MailAddress.ToString());
            mail.From = new MailAddress(unit.Owner.MailAddress);
            mail.Subject = "Your order in Vacations Paradise";
            mail.Body = "Hello and welcome to Vacations Paradise!\nHere are the details of your order:\n" + unit.ToString() + "Order create date: " + order.CreateDate + "\n";
            mail.IsBodyHtml = true;
            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("vacationsparadise18@gmail.com",
"yces24ou");
            smtp.EnableSsl = true;
            try
            {

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Here is the function

        /// <summary>
        /// Returns all orders sent to this request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<Order> Orders_Sent_To_Request(GuestRequest request)
        {
            List<Order> ordersList = Dal.Get_Order_List();
            IEnumerable<Order> result = from order in ordersList
                         where order.GuestRequestKey == request.GuestRequestKey
                         select order;
            return result.ToList<Order>();
        }
        /// <summary>
        /// Return all orders sent to a specific host
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public List<Order> Orders_Sent_To_Host(Host host)
        {
            HostingUnit temp = new HostingUnit();
            List<Order> result = new List<Order>();
            foreach(Order o in get_All_orders())
            {
                temp = get_All_units().Find(x => x.HostingUnitKey == o.HostingUnitKey);
                if (temp.Owner.HostKey == host.HostKey)
                    result.Add(o);
            }


             return result.ToList<Order>();
        }
        /// <summary>
        /// Return all orders closed with success of a specific host
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public List<Order> Orders_Closed_With_Succes_To_Host(HostingUnit unit)
        {
            List<Order> ordersList = Dal.Get_Order_List();
            IEnumerable<Order> result = from order in ordersList
                                        where order.Status == Order_Status.Mail_Sent
                                        select order;
            return result.ToList<Order>();
        }
        /// <summary>
        /// Returns orders sent by mail/closed with success to this unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int Orders_Sent_And_Closed_With_Success(HostingUnit unit)
        {
            int sum = 0;
            List<Order> orderList = Dal.Get_Order_List();
            foreach (Order order in orderList )
            {
                if (order.HostingUnitKey==unit.HostingUnitKey)
                    if (order.Status == Order_Status.Mail_Sent || order.Status == Order_Status.Closed_With_Success)
                        sum++;
            }
            return sum;
        }
        /// <summary>
        /// Print all orders
        /// </summary>
        public List<Order> get_All_orders()
        {
            List<Order> list = Dal.Get_Order_List();
            return list;
        }
        /// <summary>
        /// Search for an order using its key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Order search_Order_With_Key(int key)
        {
            List<Order> list = Dal.Get_Order_List();
            foreach(Order order in list)
            {
                if (order.OrderKey == key)
                    return order;
            }
            throw new KeyNotFoundException("This order does not exist !");
        }
        #endregion


        public List<BankBranch> Bank_List(List<BankBranch> list)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Return difference between days
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        public int difference_of_days(params DateTime[] dates)
        {
            if (dates.Length == 1)
                return (DateTime.Today - dates[0]).Days;
            else if (dates.Length == 2)
                return (dates[1] - dates[0]).Days;
            else
                throw new InvalidInputException("Invalid number of dates! From BL");
        }
        /// <summary>
        /// Function used to put the boolean values on the respective matrixes for the lists that we created on Data Source
        /// </summary>
        public void setRequests()
        {
            Dal.setRequests();
        }
        /// <summary>
        /// Search for a host using his phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Host search_Host_Using_Number(int phoneNumber)
        {
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            foreach(HostingUnit unit in list )
            {
                if (unit.Owner.PhoneNumber == phoneNumber)
                    return unit.Owner;
            }
            throw new KeyNotFoundException("This host doest not exist !");
        }
        /// <summary>
        /// Send all options of hosting unit to a client (by creating orders)
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public List<Order> send_Orders_To_Request(GuestRequest req)
        {
            List<Order> orders = new List<Order>();
            List<HostingUnit> hostList = Dal.Get_Hosting_Unit_List();
            foreach (HostingUnit unit in hostList)
            {
                if (unit.ApproveRequest(req))
                {
                    Order newOrder = new Order();
                    newOrder.HostingUnitKey = unit.HostingUnitKey;
                    newOrder.GuestRequestKey = req.GuestRequestKey;
                    newOrder.Status = Order_Status.Not_Handled;
                    newOrder.CreateDate = DateTime.UtcNow;
                    Add_Order(newOrder);
                    orders.Add(newOrder);
                }
            }
            return orders;

        }
        /// <summary>
        /// Sum of whart everyone has to pay
        /// </summary>
        /// <returns></returns>
        public int sum_To_Pay_of_Everyone()
        {
            int sum = 0;
            List<HostingUnit> list = Dal.Get_Hosting_Unit_List();
            foreach(HostingUnit unit in list)
            {
                sum += unit.Sum_to_pay;
            }
            return sum;
        }

        public List<Host> GetHosts()
        {
           return Dal.Get_Host_List();
        }
        public void Add_Host(Host host)
        {
            Dal.Add_Host(host.Clone());
        }


        public List<BankBranch> get_Bank_List()
        {
            try
            {
                return Dal.get_Bank_List();
            }
            catch(Exception ex)
            {
                throw new Exception("List is loading..");
            }
                                  

        }
        public void Update_Host(Host host)
        {
            Dal.Update_Host(host.Clone());
        }
       


        public void erase_Host(Host host)
        {
            Dal.Erase_Host(host);
        }

        public int get_Average_Of_Grades()
        {
            throw new NotImplementedException();
        }
    }
}
