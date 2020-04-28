using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BE;
using static BE.Configuration;

namespace BE
{
    public class Host
    {
        private int hostKey;
        private string privateName;
        private string familyName;
        private int phoneNumber;
        private string mailAddress;
        private BankBranch bankBranchDetails = new BankBranch();
        private int bankAccountNumber;
        private Clearance collectionClearance;
        private int numOfUnits = 0;
        private string username;
        private string password;
        private int rate = 0;
        //Constructor
        public Host() {}
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int NumOfUnits { get => numOfUnits; set => numOfUnits = value; }
        public int BankAccountNumber { get => bankAccountNumber; set => bankAccountNumber = value; }
        public int HostKey { get => hostKey; set => hostKey = value; }
        public string PrivateName { get => privateName; set => privateName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string MailAddress { get => mailAddress; set =>mailAddress = value; }
        public BankBranch BankBranchDetails { get => bankBranchDetails; set => bankBranchDetails = value; }
        public Clearance CollectionClearance { get => collectionClearance; set => collectionClearance = value; }
        public override string ToString()
        {
            string str = "";
            str += "Host key: " + HostKey + " Name: " + PrivateName + " " + FamilyName + " Phone number: " + PhoneNumber +
                " Mail address: " + MailAddress;// + " Bank branch: " + bankBranchDetails.ToString() + " Collection clearance: " + CollectionClearance.ToString() + "\r\n";

            return str;
        }
    }
}
