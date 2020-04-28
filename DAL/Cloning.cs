using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Cloning
    {
        public static List<HostingUnit> Clone(List<HostingUnit> list)
        {
            var target = list;
            return target;
        }
        public static List<Order> Clone(List<Order> list)
        {
            var target = list;
            return target;
        }
        public static List<GuestRequest> Clone(List<GuestRequest> list)
        {
            var target = list;
            return target;
        }
        public static BankBranch Clone (this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.BankName = original.BankName;
            target.BankNumber = original.BankNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;
            target.BranchNumber = original.BranchNumber;
            return target;
        }
       public static GuestRequest Clone ( this GuestRequest guestReq)
        {
            GuestRequest target = new GuestRequest();
            target.Adults = guestReq.Adults;
            target.Area = guestReq.Area;
            target.Children = guestReq.Children;
            target.ChildrensAttractions = guestReq.ChildrensAttractions;
            target.FamilyName = guestReq.FamilyName;
            target.PrivateName = guestReq.PrivateName;
            target.Garden = guestReq.Garden;
            target.GuestRequestKey = guestReq.GuestRequestKey;
            target.ID = guestReq.ID;
            target.Jacuzzi = guestReq.Jacuzzi;
            target.MailAddress = guestReq.MailAddress;
            target.Pool = guestReq.Pool;
            target.Entry_Date = guestReq.Entry_Date;
            target.PrivateName = guestReq.PrivateName;
            target.RegistrationDate = guestReq.RegistrationDate;
            target.ReleaseDate = guestReq.ReleaseDate;
            target.Status = guestReq.Status;
            target.SubArea = guestReq.SubArea;
            target.Type = guestReq.Type;
            return target;
        }
        public static Host Clone ( this Host original)
        {
            Host target = new Host();

            target.NumOfUnits = original.NumOfUnits;
            target.BankBranchDetails = original.BankBranchDetails.Clone();
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;
            target.FamilyName = original.FamilyName;
            target.HostKey = original.HostKey;
            target.MailAddress = original.MailAddress;
            target.PhoneNumber = original.PhoneNumber;
            target.PrivateName = original.PrivateName;
            target.Username = original.Username;
            target.Password = original.Password;
            return target;
        }
        public static HostingUnit Clone ( this HostingUnit hostUnit)
        {
            HostingUnit target = new HostingUnit();
            target.Adults = hostUnit.Adults;
            target.Area = hostUnit.Area;
            target.Children = hostUnit.Children;
            target.ChildrensAttractions = hostUnit.ChildrensAttractions;
            target.Garden = hostUnit.Garden;
            target.Jacuzzi = hostUnit.Jacuzzi;
            target.Type = hostUnit.Type;
            target.Pool = hostUnit.Pool;
            target.Diary = hostUnit.Diary;
            target.HostingUnitKey = hostUnit.HostingUnitKey;
            target.HostingUnitName = hostUnit.HostingUnitName;
            target.Owner = hostUnit.Owner.Clone();
            return target;
        }
        public static Order Clone ( this Order original)
        {
            Order target = new Order();
            target.CreateDate = original.CreateDate;
            target.GuestRequestKey = original.GuestRequestKey;
            target.HostingUnitKey = original.HostingUnitKey;
            target.OrderDate = original.OrderDate;
            target.OrderKey = original.OrderKey;
            target.Status = original.Status;
            return target;
        }
    

    }
}
