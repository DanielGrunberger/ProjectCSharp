using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using DS;
using BE;
using System.Net;
using System.Xml;
using System.Globalization;
using System.Net.Mail;

namespace DAL
{
    class imp_XML_Dal : Idal
    {
        public static List<HostingUnit> hostingUnitList;
        XElement orderRoot;
        XElement GuestRequestRoot;
        XElement configRoot;
        XElement hostingUnitRoot;
        XElement bankRoot;
        XElement hostRoot;
        string hostRootPath = @"hostsXML.xml";
        string guestRequestPath = @"GuestRequestXML.xml";
        string bankPath = @"atm.xml";
        string orderPath = @"OrderXML.xml";
        string HostingUnitPath = @"HostingUnitsXML.xml";
        string configPath = @"ConfigurationXML.xml";
        BackgroundWorker bg = new BackgroundWorker();
        const string xmlLocalPath = @"atm.xml";
        WebClient wc = new WebClient();

        public imp_XML_Dal()
        {
            try
            {
                if (!File.Exists(orderPath))
                {
                    orderRoot = new XElement("orders");
                    orderRoot.Save(orderPath);
                }
                else
                    LoadDataOrder();

                if (!File.Exists(configPath))
                    CreateFilesCode();
                LoadDataCode();


                if (!File.Exists(guestRequestPath))
                {
                    GuestRequestRoot = new XElement("guestRequests");
                    GuestRequestRoot.Save(guestRequestPath);
                }
                else
                    LoadDataGuestRequest();

                if (!File.Exists(HostingUnitPath))
                {
                    hostingUnitRoot = new XElement("hostingUnits");
                    hostingUnitRoot.Save(HostingUnitPath);
                    SaveToXML(new List<HostingUnit>(), HostingUnitPath);
                }
                else
                    LoadDataHostingUnit();
                if (!File.Exists(hostRootPath))
                {
                    hostRoot = new XElement("hosts");
                    hostRoot.Save(hostRootPath);
                }
                else
                    LoadDataHost();
                if (!File.Exists(bankPath))
                {
                    bankRoot = new XElement("banks");
                    hostRoot.Save(bankPath);

                }
                else
                    LoadBankData();


                bg.DoWork += Bg_DoWork;
                bg.RunWorkerAsync();


               hostingUnitList = LoadFromXML<List<HostingUnit>>(HostingUnitPath);


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ~imp_XML_Dal()
        {
            try
            {
                XElement guestRequest_Key = new XElement("RequestKey", Configuration.GuestRequest_Key);
                XElement host_Key = new XElement("HostKey", Configuration.Host_Key);
                XElement hostingUnit_Key = new XElement("UnitKey", Configuration.HostingUnit_Key);
                XElement order_Key = new XElement("OrderKey", Configuration.Order_Key);
                XElement comission = new XElement("comission", 10);
                configRoot = new XElement("Config", guestRequest_Key, host_Key, hostingUnit_Key, order_Key, comission);
                configRoot.Save(configPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #region   save and load from xml
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
        #endregion

        #region bank
        private void Bg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadBankData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void LoadBankData()
        {
            
                try
                {

                    string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);



                }
                catch (Exception)
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                finally
                {
                    wc.Dispose();
                }

            
            bankRoot = XElement.Load(xmlLocalPath);
        }
        public List<BankBranch> get_Bank_List()
        {
            try
            {


                if (bankRoot == null)
                    throw new InvalidOperationException("list is loading");
                var it = (from item in bankRoot.Elements()
                          select ConvertBank(item).Clone()).ToList();
                return it;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// converts bank to type of bank branches from xml
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        BankBranch ConvertBank(XElement element)
        {
            try
            {

                BankBranch bank = new BankBranch();
              
                bank.BankName = element.Element("שם_בנק").Value;
                bank.BankNumber = int.Parse(element.Element("קוד_בנק").Value);
                bank.BranchNumber = int.Parse(element.Element("קוד_סניף").Value);
                bank.BranchAddress = element.Element("כתובת_ה-ATM").Value;
                bank.BranchCity = element.Element("ישוב").Value;
                return bank;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion








        private void LoadDataHostingUnit()
        {
            try
            {
                hostingUnitRoot = XElement.Load(HostingUnitPath);

            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }

        private void LoadDataCode()
        {
            try
            {
                configRoot = XElement.Load(configPath);
                Configuration.Comission = int.Parse(configRoot.Element("comission").Value);
                Configuration.GuestRequest_Key= int.Parse(configRoot.Element("RequestKey").Value);
                Configuration.HostingUnit_Key = int.Parse(configRoot.Element("UnitKey").Value);
                Configuration.Host_Key = int.Parse(configRoot.Element("HostKey").Value);
                Configuration.Order_Key = int.Parse(configRoot.Element("OrderKey").Value);
            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }

        private void CreateFilesCode()
        {
            try
            {
                XElement guestRequest_Key = new XElement("RequestKey", Configuration.GuestRequest_Key);
                XElement host_Key = new XElement("HostKey", Configuration.Host_Key);
                XElement hostingUnit_Key = new XElement("UnitKey", Configuration.HostingUnit_Key);
                XElement order_Key = new XElement("OrderKey", Configuration.Order_Key);
                XElement comission = new XElement("comission", 10);
                configRoot = new XElement("Config", guestRequest_Key,host_Key, hostingUnit_Key, order_Key, comission);
                configRoot.Save(configPath);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //====================================================================================================================
     
        //====================================================================================================================

        #region Guestrequest

        public void GuestRequestsManagement()
        {
            if (!File.Exists(guestRequestPath))
                CreateFileGuestRequests();
            LoadDataGuestRequest();
        }
        private void LoadDataGuestRequest()
        {
            try
            {
                GuestRequestRoot = XElement.Load(guestRequestPath);

            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }
        private void CreateFileGuestRequests()
        {
            GuestRequestRoot = new XElement("guestRequests");
            GuestRequestRoot.Save(guestRequestPath);
        }
        /// <summary>
        ///adding a new request
        /// </summary>
        /// <param name="guestRequest"></param>
        public void Add_Guest_Request(GuestRequest guestRequest)
        {
            XElement request;
          
                request = (from gr in GuestRequestRoot.Elements()
                           where Int32.Parse(gr.Element("ID").Value) == guestRequest.ID
                           select gr).FirstOrDefault();
            

            if (request != null)
            {
               
                Update_Guest_Request(guestRequest);
             
            }
            guestRequest.GuestRequestKey = Configuration.GuestRequest_Key;
            Configuration.GuestRequest_Key++;
            GuestRequestRoot.Add(new XElement("guestRequest",
                                      new XElement("GuestRequestKey", guestRequest.GuestRequestKey),
                                      new XElement("ID", guestRequest.ID),
                                      new XElement("PrivateName", guestRequest.PrivateName),
                                      new XElement("FamilyName", guestRequest.FamilyName),
                                      new XElement("MailAddress", guestRequest.MailAddress),
                                      new XElement("Status", guestRequest.Status),
                                      new XElement("RegistrationDate", guestRequest.RegistrationDate),
                                      new XElement("Entry_Date", guestRequest.Entry_Date),
                                      new XElement("ReleaseDate", guestRequest.ReleaseDate),
                                      new XElement("Area", guestRequest.Area),
                                      new XElement("SubArea", guestRequest.SubArea),
                                      new XElement("Type", guestRequest.Type),
                                      new XElement("Adults", guestRequest.Adults),
                                      new XElement("Children", guestRequest.Children),
                                      new XElement("Pool", guestRequest.Pool),
                                      new XElement("Jacuzzi", guestRequest.Jacuzzi),
                                      new XElement("Garden", guestRequest.Garden),
                                      new XElement("ChildrensAttractions", guestRequest.ChildrensAttractions)
                                    )
                );


            GuestRequestRoot.Save(guestRequestPath);
        }




        /// <summary>
        /// updating a request
        /// </summary>
        /// <param name="guestRequest"></param>
        /// <param name="status"></param>
        public void Update_Guest_Request(GuestRequest guestRequest)
        {
            XElement tempElement = (from gr in GuestRequestRoot.Elements()
                                    where int.Parse(gr.Element("ID").Value) == guestRequest.ID
                                    select gr).FirstOrDefault();
            if (tempElement == null)
            {
                throw new RequestNotFoundException();
            }
            tempElement.Element("GuestRequestKey").Value = guestRequest.GuestRequestKey.ToString();
            tempElement.Element("ID").Value = guestRequest.ID.ToString();
            tempElement.Element("PrivateName").Value = guestRequest.PrivateName;
            tempElement.Element("FamilyName").Value = guestRequest.FamilyName;
            tempElement.Element("MailAddress").Value = guestRequest.MailAddress.ToString();
            tempElement.Element("Status").Value = guestRequest.Status.ToString();
            tempElement.Element("RegistrationDate").Value = guestRequest.RegistrationDate.ToString();
            tempElement.Element("Entry_Date").Value = guestRequest.Entry_Date.ToString();
            tempElement.Element("ReleaseDate").Value = guestRequest.ReleaseDate.ToString();
            tempElement.Element("Area").Value = guestRequest.Area.ToString();
            tempElement.Element("SubArea").Value = guestRequest.SubArea;
            tempElement.Element("Type").Value = guestRequest.Type.ToString();
            tempElement.Element("Adults").Value = guestRequest.Adults.ToString();
            tempElement.Element("Children").Value = guestRequest.Children.ToString();
            tempElement.Element("Pool").Value = guestRequest.Pool.ToString();
            tempElement.Element("Jacuzzi").Value = guestRequest.Jacuzzi.ToString();
            tempElement.Element("Garden").Value = guestRequest.Garden.ToString();
            tempElement.Element("ChildrensAttractions").Value = guestRequest.ChildrensAttractions.ToString();

            GuestRequestRoot.Save(guestRequestPath);
        }
        /// <summary>
        ///Getting the list of requests 
        /// </summary>
        /// <returns></returns>
        public List<GuestRequest> Get_Guest_Request_List()
        {

            LoadDataGuestRequest();
            var it = (from item in GuestRequestRoot.Elements()
                      select ConvertGuestRequest(item).Clone()).ToList();
            return it;
        }

            XElement ConvertGuestRequest(GuestRequest request)
        {
            try
            {
                XElement requestElement = new XElement("guestRequest");
                requestElement.Add(new XElement("GuestRequestKey", request.GuestRequestKey));
                requestElement.Add(new XElement("ID", request.ID));
                requestElement.Add(new XElement("PrivateName", request.PrivateName));
                requestElement.Add(new XElement("FamilyName", request.FamilyName));
                requestElement.Add(new XElement("MailAddress", request.MailAddress));
                requestElement.Add(new XElement("Status", request.Status));
                requestElement.Add(new XElement("RegistrationDate", request.RegistrationDate));
                requestElement.Add(new XElement("Entry_Date", request.Entry_Date));
                requestElement.Add(new XElement("ReleaseDate", request.ReleaseDate));
                requestElement.Add(new XElement("Area", request.Area));
                requestElement.Add(new XElement("Type", request.Type));
                requestElement.Add(new XElement("Adults", request.Adults));
                requestElement.Add(new XElement("Children", request.Children));
                requestElement.Add(new XElement("Pool", request.Pool));
                requestElement.Add(new XElement("Jacuzzi", request.Jacuzzi));
                requestElement.Add(new XElement("Garden", request.Garden));
                requestElement.Add(new XElement("ChildrensAttractions", request.ChildrensAttractions));


                return requestElement;
            }
            catch (Exception e)
            {
                throw new InvalidDataException("problem coverting guest request for xml file");
            }
        }
        /// <summary>
        /// converts xml element to guest request
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        GuestRequest ConvertGuestRequest(XElement element)
        {
            try
            {
                GuestRequest request = new GuestRequest();
                request.GuestRequestKey = int.Parse(element.Element("GuestRequestKey").Value);
                request.PrivateName = element.Element("PrivateName").Value;
                request.ID = int.Parse(element.Element("ID").Value);
                request.FamilyName = element.Element("FamilyName").Value;
                request.MailAddress =element.Element("MailAddress").Value;
                request.Status = (Guest_Request_Status)Enum.Parse(typeof(Guest_Request_Status), element.Element("Status").Value);
                request.RegistrationDate = DateTime.Parse(element.Element("RegistrationDate").Value);
                request.Entry_Date = DateTime.Parse(element.Element("Entry_Date").Value);
                request.ReleaseDate = DateTime.Parse(element.Element("ReleaseDate").Value);
                request.Area = (Area)Enum.Parse(typeof(Area), element.Element("Area").Value);
                request.Type = (type)Enum.Parse(typeof(type), element.Element("Type").Value);
                request.Adults = int.Parse(element.Element("Adults").Value);
                request.Children = int.Parse(element.Element("Children").Value);
                request.Pool = (Interested)Enum.Parse(typeof(Interested), element.Element("Pool").Value);
                request.Jacuzzi = (Interested)Enum.Parse(typeof(Interested), element.Element("Jacuzzi").Value);
                request.Garden = (Interested)Enum.Parse(typeof(Interested), element.Element("Garden").Value);
                request.ChildrensAttractions = (Interested)Enum.Parse(typeof(Interested), element.Element("ChildrensAttractions").Value);
                return request;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
       


        public void AddGuestRequestList()
        {
            foreach (GuestRequest gr in DataSource.guestRequestList)
            {
                Add_Guest_Request(gr);
            }
        }
        #endregion

        #region Order

        private void LoadDataOrder()
        {
            try
            {
                orderRoot = XElement.Load(orderPath);

            }
            catch
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }
        private void CreateFileOrders()
        {
            orderRoot = new XElement("Orders");
            orderRoot.Save(orderPath);
        }
        /// <summary>
        /// Adding new order
        /// </summary>
        /// <param name="order"></param>
        public void Add_Order(Order order)
        {
            XElement unit = (from gr in hostingUnitRoot.Elements()
                             where int.Parse(gr.Element("HostingUnitKey").Value) == order.HostingUnitKey
                             select gr).FirstOrDefault();
            if (unit == null)
                throw new KeyNotFoundException("There is not such hosting unit ! From Dal");

            XElement req = (from gr in GuestRequestRoot.Elements()
                            where int.Parse(gr.Element("GuestRequestKey").Value) == order.GuestRequestKey
                            select gr).FirstOrDefault();
            if (req == null)
                throw new KeyNotFoundException("There is no such guest request ! From Dal");

            order.OrderKey = Configuration.Order_Key;
            orderRoot.Add(new XElement("Orders",
                                            new XElement("OrdeKey", order.OrderKey),
                                            new XElement("HostingUnitKey", order.HostingUnitKey),
                                            new XElement("GuestRequestKey", order.GuestRequestKey),
                                            new XElement("Status", order.Status),
                                            new XElement("CreateDate", order.CreateDate),
                                            new XElement("OrderDate", order.OrderDate)
                                            )
                                           );
            orderRoot.Save(orderPath);
        }
        /// <summary>
        /// Updating order
        /// </summary>
        /// <param name="order"></param>
        public void Update_Order(Order order)
        {
            XElement tempElement = (from gr in orderRoot.Elements()
                                    where int.Parse(gr.Element("OrdeKey").Value) == order.OrderKey
                                    select gr).FirstOrDefault();
            if (tempElement == null)
            {
                throw new KeyNotFoundException("The order does not exist!");
            }
            tempElement.Element("OrdeKey").Value = order.OrderKey.ToString();
            tempElement.Element("HostingUnitKey").Value = order.HostingUnitKey.ToString();
            tempElement.Element("GuestRequestKey").Value = order.GuestRequestKey.ToString();
            tempElement.Element("Status").Value = order.Status.ToString();
            tempElement.Element("CreateDate").Value = order.CreateDate.ToString();
            tempElement.Element("OrderDate").Value = order.OrderDate.ToString();

            orderRoot.Save(orderPath);
        }
        XElement ConvertOrder(BE.Order order)
        {
            try
            {
                XElement OrderElement = new XElement("order");
                OrderElement.Add(new XElement("GuestRequestKey", order.GuestRequestKey));
                OrderElement.Add(new XElement("HostingUnitKey", order.HostingUnitKey));
                OrderElement.Add(new XElement("OrdeKey", order.OrderKey));
                OrderElement.Add(new XElement("Status", order.Status));
                OrderElement.Add(new XElement("CreateDate", order.CreateDate));
                OrderElement.Add(new XElement("OrderDate", order.OrderDate));
                return OrderElement;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// converts xml element to an order
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        BE.Order ConvertOrder(XElement element)
        {
            try
            {
                Order order = new BE.Order();

                order.GuestRequestKey = int.Parse((element.Element("GuestRequestKey").Value));
                order.HostingUnitKey = int.Parse((element.Element("HostingUnitKey").Value));
                order.OrderKey = int.Parse((element.Element("OrdeKey").Value));
                order.Status = (Order_Status)Enum.Parse(typeof(Order_Status), element.Element("Status").Value);
                order.OrderDate = Convert.ToDateTime((element.Element("OrderDate").Value));
                order.CreateDate = Convert.ToDateTime((element.Element("CreateDate").Value));

                return order;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Getting list of orders
        /// </summary>
        /// <returns></returns>
        public List<Order> Get_Order_List()
        {
            try
            {
                LoadDataOrder();
                var it = (from item in orderRoot.Elements()
                          select ConvertOrder(item).Clone()).ToList();
                return it;
            }
            catch (Exception e)
            {
                throw new InvalidDataException("problem loading order list");
            }
        }

        public void AddOrderList()
        {
            foreach (Order ord in DataSource.orderList)
            {
                Add_Order(ord);
            }
        }

        #endregion

        #region Host
      
       private void LoadDataHost()
        {
            try
            {
                hostRoot = XElement.Load(hostRootPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("problem uploading file");
            }
        }



        Host ConvertHost(XElement element)
        {
            Host host = new Host();
            host.HostKey = int.Parse(element.Element("HostKey").Value);
            host.PrivateName = element.Element("PrivateName").Value;
            host.FamilyName = element.Element("FamilyName").Value;
            host.MailAddress = element.Element("MailAddress").Value;
            host.PhoneNumber = int.Parse(element.Element("PhoneNumber").Value);
            host.NumOfUnits = int.Parse(element.Element("NumOfUnits").Value);
            host.Username = element.Element("Username").Value;
            host.Password = element.Element("Password").Value;
            host.BankBranchDetails.BankName = element.Element("BankBranchDetails").Element("BankName").Value;
            host.BankBranchDetails.BankNumber = int.Parse(element.Element("BankBranchDetails").Element("BankNumber").Value);
            host.BankBranchDetails.BranchNumber = int.Parse(element.Element("BankBranchDetails").Element("BranchNumber").Value);
            host.BankBranchDetails.BranchCity = element.Element("BankBranchDetails").Element("BranchCity").Value;
            host.BankBranchDetails.BranchAddress = element.Element("BankBranchDetails").Element("BranchAddress").Value;
            return host;


        }

        /// <summary>
        ///adding a new host
        /// </summary>
        /// <param name="host"></param>
        public void Add_Host(Host host)
        {
            XElement host_h = (from hos in hostRoot.Elements()
                               where int.Parse(hos.Element("HostKey").Value) == host.HostKey
                               select hos).FirstOrDefault();
            if (host_h != null)
            {
                host.HostKey = Configuration.Host_Key;
                Update_Host(host);
            }
            host.HostKey = Configuration.Host_Key;
            hostRoot.Add(new XElement("Hosts",
                             new XElement("HostKey", host.HostKey),
                             new XElement("PrivateName", host.PrivateName),
                             new XElement("FamilyName", host.FamilyName),
                             new XElement("PhoneNumber", host.PhoneNumber),
                             new XElement("MailAddress", host.MailAddress),
                             new XElement("NumOfUnits", host.NumOfUnits),
                             new XElement("Username", host.Username),
                             new XElement("Password", host.Password),
                             new XElement("BankBranchDetails",
                                           new XElement("BankNumber", host.BankBranchDetails.BankNumber),
                                           new XElement("BankName", host.BankBranchDetails.BankName),
                                           new XElement("BranchNumber", host.BankBranchDetails.BranchNumber),
                                           new XElement("BranchCity", host.BankBranchDetails.BranchCity),
                                           new XElement("BranchAddress", host.BankBranchDetails.BranchAddress)
                                           ),
                             new XElement("BankAccountNumber", host.BankAccountNumber),
                             new XElement("CollectionClearance", host.CollectionClearance)
                                         )
                            );
            hostRoot.Save(hostRootPath);
        }
        /// <summary>
        /// Updating Host
        /// </summary>
        /// <param name="host"></param>
        public void Update_Host(Host host)
        {
            XElement tempElement = (from hos in hostRoot.Elements()
                                    where int.Parse(hos.Element("HostKey").Value) == host.HostKey
                                    select hos).FirstOrDefault();
            if (tempElement == null)
            {
                throw new KeyNotFoundException("The host does not exist!");
            }
            tempElement.Element("HostKey").Value = host.HostKey.ToString();
            tempElement.Element("PrivateName").Value = host.PrivateName;
            tempElement.Element("FamilyName").Value = host.FamilyName;
            tempElement.Element("PhoneNumber").Value = host.PhoneNumber.ToString();
            tempElement.Element("MailAddress").Value = host.MailAddress;
            tempElement.Element("NumOfUnits").Value = host.NumOfUnits.ToString();
            tempElement.Element("Username").Value = host.Username;
            tempElement.Element("Password").Value = host.Password;
            tempElement.Element("BankBranchDetails").Element("BankNumber").Value = host.BankBranchDetails.BankNumber.ToString();
            tempElement.Element("BankBranchDetails").Element("BankName").Value = host.BankBranchDetails.BankName;
            tempElement.Element("BankBranchDetails").Element("BranchNumber").Value = host.BankBranchDetails.BranchNumber.ToString();
            tempElement.Element("BankBranchDetails").Element("BranchAddress").Value = host.BankBranchDetails.BranchAddress;
            tempElement.Element("BankBranchDetails").Element("BranchCity").Value = host.BankBranchDetails.BranchCity;
            tempElement.Element("BankAccountNumber").Value = host.BankAccountNumber.ToString();
            tempElement.Element("CollectionClearance").Value = host.CollectionClearance.ToString();


            hostRoot.Save(hostRootPath);
        }
        /// <summary>
        /// Erasing host unit
        /// </summary>
        /// <param name="host"></param>
        public void Erase_Host(Host host)
        {
            var host_to_erase = (from h in hostRoot.Elements()
                               where int.Parse(h.Element("HostKey").Value) == host.HostKey
                               select h);
            host_to_erase.Remove();
            hostRoot.Save(hostRootPath);
        }
        public List<Host> Get_Host_List()
        {
            LoadDataHost();
            List<Host> List = (from h in hostRoot.Elements()
                               select new Host()
                               {
                                   HostKey = Int32.Parse(h.Element("HostKey").Value),
                                   PrivateName = h.Element("PrivateName").Value,
                                   FamilyName = h.Element("FamilyName").Value,
                                   PhoneNumber = Int32.Parse(h.Element("PhoneNumber").Value),
                                   MailAddress = h.Element("MailAddress").Value,
                                   NumOfUnits = Int32.Parse(h.Element("NumOfUnits").Value),
                                   Username = h.Element("Username").Value,
                                   Password = h.Element("Password").Value,
                                   BankBranchDetails = new BankBranch()
                                   {
                                       BankNumber = Int32.Parse(h.Element("BankBranchDetails").Element("BankNumber").Value),
                                       BankName = h.Element("BankBranchDetails").Element("BankName").Value,
                                       BranchNumber = Int32.Parse(h.Element("BankBranchDetails").Element("BranchNumber").Value),
                                       BranchAddress = h.Element("BankBranchDetails").Element("BranchAddress").Value,
                                       BranchCity = h.Element("BankBranchDetails").Element("BranchCity").Value
                                   },
                                   BankAccountNumber = Int32.Parse(h.Element("BankAccountNumber").Value),
                                   CollectionClearance = (Clearance)Enum.Parse(typeof(Clearance), h.Element("CollectionClearance").Value)
                               }
                             ).ToList();
            return List;
        }
      
        #endregion

        #region hostingUnit
        /// <summary>
        /// Adding new hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Add_Hosting_Unit(HostingUnit hostUnit)
        {
            var unit = from units in hostingUnitList
                       let newUnit = units
                       where units.HostingUnitKey == hostUnit.HostingUnitKey || units.HostingUnitName == hostUnit.HostingUnitName
                       select units;
            List<Host> host = (from h in hostRoot.Elements()
                               where int.Parse(h.Element("HostKey").Value) == hostUnit.Owner.HostKey
                               select ConvertHost(h)).ToList();
            host[0].NumOfUnits++;
            if (unit.FirstOrDefault() == null)
            {

                hostUnit.HostingUnitKey = Configuration.HostingUnit_Key;
                hostUnit.Owner.NumOfUnits++;

                Update_Host(hostUnit.Owner);
                hostingUnitList.Add(hostUnit);
                SaveToXML(hostingUnitList, HostingUnitPath);
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
            try { 
            LoadDataHostingUnit();
            var it = (from item in hostingUnitRoot.Elements()
                      where int.Parse(item.Element("HostingUnitKey").Value) == hostUnit.HostingUnitKey
                      select item).FirstOrDefault();

                List<Host> host = (from h in hostRoot.Elements()
                            where int.Parse(h.Element("HostKey").Value) == hostUnit.Owner.HostKey
                            select ConvertHost(h)).ToList();
                host[0].NumOfUnits--;

              
             it.Remove();
            hostingUnitRoot.Save(HostingUnitPath);
        }
            catch (Exception e)
            {
                throw new KeyNotFoundException(" cannot delete hosting unit");
            }

       }
        /// <summary>
        /// Updating hosting unit
        /// </summary>
        /// <param name="hostUnit"></param>
        public void Update_Hosting_Unit(HostingUnit hostUnit)
        {
            HostingUnit temp = new HostingUnit();
            foreach (HostingUnit i in hostingUnitList)
            {
                if (i.HostingUnitKey==hostUnit.HostingUnitKey)
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

                    SaveToXML(hostingUnitList, HostingUnitPath);
                    return;
                }
            }
            throw new KeyNotFoundException();
        }

        XElement xConvertHostingUnit(HostingUnit unit)
        {
            try
            {
                var doc = new XDocument();
                using (XmlWriter writer = doc.CreateWriter())
                {
                    XmlSerializer ser = new XmlSerializer(unit.GetType());
                    ser.Serialize(writer, unit);
                }
                return doc.Root;

            }
            catch (Exception e)

            {
                throw e;
            }
        }
        /// <summary>
        /// converts xml element to a hosting unit
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        BE.HostingUnit ConvertHostingUnit(XElement element)
        {
            try
            {

                HostingUnit unit = new HostingUnit();
                var ser = new XmlSerializer(typeof(HostingUnit));
                return (HostingUnit)ser.Deserialize(element.CreateReader());

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Getting the list of hosting units
        /// </summary>
        /// <returns></returns>
        public List<HostingUnit> Get_Hosting_Unit_List()
        {
            try
            {
                LoadDataHostingUnit();
                var it = (from item in hostingUnitRoot.Elements()
                          select ConvertHostingUnit(item).Clone()).ToList();
                return it;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
      
        public void setRequests()
        {
            DataSource.hostingUnitList[0].setRequest(DataSource.guestRequestList[0]);
            DataSource.hostingUnitList[1].setRequest(DataSource.guestRequestList[1]);
            DataSource.hostingUnitList[2].setRequest(DataSource.guestRequestList[2]);
        }

      

      
    }

}

