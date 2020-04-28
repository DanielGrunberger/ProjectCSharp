using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
   public  class Dal_imp : Idal
    {
        #region Guest Request
        /// <summary>
        ///adding a new request
        /// </summary>
        /// <param name="guestRequest"></param>
        public  void Add_Guest_Request(GuestRequest guestRequest)
        {
            var request= from gr in DataSource.guestRequestList
            let req = gr where gr.ID == guestRequest.ID
            select req;
            if (request.FirstOrDefault() == null)
            {
                guestRequest.GuestRequestKey = Configuration.GuestRequest_Key;
                DataSource.guestRequestList.Add(guestRequest);
                return;
            }
            else
            {
                guestRequest.GuestRequestKey = DataSource.guestRequestList.Find(o => o.ID == guestRequest.ID).GuestRequestKey;
                Update_Guest_Request(guestRequest);
            }
        }
        /// <summary>
        /// updating a request
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <param name="status"></param>
        public void Update_Guest_Request(GuestRequest guestReq)
        {
            GuestRequest target = DataSource.guestRequestList.Find(o=>o.ID==guestReq.ID);
            if (target != null)
            { 
                    target.Adults = guestReq.Adults;
                    target.Area = guestReq.Area;
                    target.Children = guestReq.Children;
                    target.ChildrensAttractions = guestReq.ChildrensAttractions;
                    target.FamilyName = guestReq.FamilyName;
                    target.PrivateName = guestReq.PrivateName;
                    target.Garden = guestReq.Garden;
                    target.GuestRequestKey = guestReq.GuestRequestKey;
                    target.Jacuzzi = guestReq.Jacuzzi;
                    target.MailAddress = guestReq.MailAddress;
                    target.Pool = guestReq.Pool;
                    target.Entry_Date = guestReq.Entry_Date;
                    target.RegistrationDate = guestReq.RegistrationDate;
                    target.ReleaseDate = guestReq.ReleaseDate;
                    target.Status = guestReq.Status;
                    target.SubArea = guestReq.SubArea;
                    target.Type = guestReq.Type;
                    return;
            }            
            throw new RequestNotFoundException();
        }
        /// <summary>
        ///Getting the list of requests 
        /// </summary>
        /// <returns></returns>
        public List<GuestRequest> Get_Guest_Request_List()
        {
            List<GuestRequest> list = (from req in DataSource.guestRequestList
                                       select req.Clone()).ToList();
            return list;
        }
        #endregion

        #region Hosting Unit
        /// <summary>
        /// Adding new hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Add_Hosting_Unit(HostingUnit hostUnit)
        {
            
            var unit = from units in DataSource.hostingUnitList
                          let newUnit = units
                          where units.HostingUnitKey == hostUnit.HostingUnitKey || units.HostingUnitName==hostUnit.HostingUnitName
                          select  units;
            if (unit.FirstOrDefault() == null)
            {
                hostUnit.HostingUnitKey = Configuration.HostingUnit_Key;
                hostUnit.Owner.NumOfUnits++;
                /* HostingUnit HostUnit = DataSource.hostList.Find(o => o.PrivateName == hostUnit.Owner.PrivateName && o.FamilyName == hostUnit.Owner.FamilyName);
                 if (HostUnit != null)
                 {
                     hostUnit.Owner.HostKey = HostUnit.Owner.HostKey;
                     DataSource.hostingUnitList.Add(hostUnit);
                     return;
                 }*/


                // hostUnit.Owner.HostKey = Configuration.Host_Key;
                Update_Host(hostUnit.Owner);
                DataSource.hostingUnitList.Add(hostUnit);
                return;
            }
            else
                Update_Hosting_Unit(hostUnit);
           
        }
        /// <summary>
        /// Erasing hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Erase_Hosting_Unit(HostingUnit hostUnit)
        {
            HostingUnit unit = DataSource.hostingUnitList.Find(o => o.HostingUnitKey == hostUnit.HostingUnitKey);
            
            if (unit!=null)
            { 
                    DataSource.hostingUnitList.Remove(unit);
                Host host = DataSource.hostList.Find(x => x.HostKey == hostUnit.Owner.HostKey);
                host.NumOfUnits--;
                return;
             }

            
            throw new KeyNotFoundException();  

        }
        /// <summary>
        /// Updating hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Update_Hosting_Unit(HostingUnit hostUnit)
        {
            HostingUnit temp = new HostingUnit();
            foreach (HostingUnit i in DataSource.hostingUnitList)
            {
                if (i.Owner.PrivateName== hostUnit.Owner.PrivateName && i.Owner.FamilyName==hostUnit.Owner.FamilyName)
                {
                    temp = i;
                    temp.HostingUnitName = hostUnit.HostingUnitName;
                    temp.HostingUnitKey = hostUnit.HostingUnitKey;
                    temp.Jacuzzi = hostUnit.Jacuzzi;
                    temp.Owner = hostUnit.Owner;
                    temp.Pool = hostUnit.Pool;
                    temp.SubArea = hostUnit.SubArea;
                    temp.Type = hostUnit.Type;
                    temp.Adults = hostUnit.Adults;
                    temp.Area = hostUnit.Area;
                    temp.Children = hostUnit.Children;
                    temp.ChildrensAttractions = hostUnit.ChildrensAttractions;
                    temp.Diary = hostUnit.Diary;
                    return;
                }
            }
            throw new KeyNotFoundException();
        }
        /// <summary>
        /// Getting the list of hosting units
        /// </summary>
        /// <returns></returns>
        public List<HostingUnit> Get_Hosting_Unit_List()
        {
            List<HostingUnit> list = (from unit in DataSource.hostingUnitList
                                      select unit.Clone()).ToList();
            return list;
        }
        #endregion

        public List<Host> Get_Host_List()
        {
            List<Host> list =(from host in DataSource.hostList
                              select host.Clone()).ToList();
            return list;
        }

        #region Order
        /// <summary>
        /// Adding new order
        /// </summary>
        /// <param name="order"></param>
        public void Add_Order(Order order)
        {
            List<HostingUnit> hostList = DataSource.hostingUnitList;
            IEnumerable<HostingUnit> unit = from HostingUnit hUnit in hostList
                                            where hUnit.HostingUnitKey == order.HostingUnitKey
                                            select hUnit;
            if (unit.FirstOrDefault() == null)
                throw new KeyNotFoundException("There is not such hosting unit ! From Dal");
            List<GuestRequest> reqList = DataSource.guestRequestList;
            IEnumerable<GuestRequest> req= from GuestRequest gr in reqList
                                           where gr.GuestRequestKey==order.GuestRequestKey
                                           select gr;
            if (req.FirstOrDefault() == null)
                throw new KeyNotFoundException("There is no such guest request ! From Dal");

            order.OrderKey = Configuration.Order_Key;
            DataSource.orderList.Add(order);
        }   
        /// <summary>
        /// Getting list of orders
        /// </summary>
        /// <returns></returns>
        public List<Order> Get_Order_List()
        {
            List<Order> list = (from order in DataSource.orderList
                                select order.Clone()).ToList();
            return list;
        }
        /// <summary>
        /// Updating order
        /// </summary>
        /// <param name="order"></param>
        public void Update_Order(Order order)
        {
            Order Order = DataSource.orderList.Find(o => o.OrderKey == order.OrderKey);
                if (Order != null)
                {
                Order.GuestRequestKey = order.GuestRequestKey;
                Order.HostingUnitKey = order.HostingUnitKey;
                Order.OrderDate = order.OrderDate;
                Order.Status = order.Status;
                Order.CreateDate = order.CreateDate;
                return;
                }
            
            throw new KeyNotFoundException("The order does not exist!");
        }
        #endregion

        /// <summary>
        /// Function to start the matrix value of data source
        /// </summary>
        public void setRequests()
        {
            DataSource.hostingUnitList[0].setRequest(DataSource.guestRequestList[0]);
            DataSource.hostingUnitList[1].setRequest(DataSource.guestRequestList[1]);
            DataSource.hostingUnitList[2].setRequest(DataSource.guestRequestList[2]);
        }
        public List<BankBranch> get_Bank_List()
        {
           List<BankBranch> list = new List<BankBranch>()
            {
                new BankBranch()
                {
                    BankName = "Hapoalim",
                    BankNumber = 12,
                    BranchAddress="Kikar Hashabat 1",
                    BranchCity="Jerusalem",
                    BranchNumber= 657
                },
                new BankBranch()
                {
                    BankName= "Discount",
                    BankNumber= 92,
                    BranchNumber=643,
                    BranchCity="Ashdod",
                    BranchAddress="Yrmiahu 845"
                },
                new BankBranch()
                {
                    BankName= "Leumi",
                    BankNumber= 64,
                    BranchNumber=500,
                    BranchCity="Jerusalem",
                    BranchAddress="Mahane Yehuda 845"
                },
                new BankBranch()
                {
                    BankName= "pagi",
                    BankNumber= 16,
                    BranchNumber=145,
                    BranchCity="Rishon letzion",
                    BranchAddress="shemoel tamir 2"
                },
                new BankBranch()
                {
                    BankName= "yahav",
                    BankNumber= 26,
                    BranchNumber=188,
                    BranchCity="teverya",
                    BranchAddress="harambam 26"
                }
            };
            return list;
        }

        public void Erase_Host(Host host)
        {
            Host host_to_erase = DataSource.hostList.Find(x => x.HostKey == host.HostKey);
            DataSource.hostList.Remove(host_to_erase);
            for (int i = DataSource.hostingUnitList.Count-1;i>=0;i--)
            {
                if (DataSource.hostingUnitList[i].Owner.HostKey == host.HostKey)
                    DataSource.hostingUnitList.RemoveAt(i);
            }
            
        }
            




        public  void Add_Host(Host host)
        {
            host.HostKey = Configuration.Host_Key;
            DataSource.hostList.Add(host);
        }
        public void Update_Host(Host host)
        {
            Host host_to_update =DataSource.hostList.Find(x => x.HostKey == host.HostKey);
            host_to_update.NumOfUnits = host.NumOfUnits;
            host_to_update.BankBranchDetails = host.BankBranchDetails.Clone();
            host_to_update.BankAccountNumber = host.BankAccountNumber;
            host_to_update.CollectionClearance = host.CollectionClearance;
            host_to_update.FamilyName = host.FamilyName;
            host_to_update.HostKey = host.HostKey;
            host_to_update.MailAddress = host.MailAddress;
            host_to_update.PhoneNumber = host.PhoneNumber;
            host_to_update.PrivateName = host.PrivateName;
            host_to_update.Username = host.Username;
            host_to_update.Password = host.Password;
        }
    }
}
