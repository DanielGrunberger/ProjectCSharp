using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        private int bankNumber;
        private string bankName;
        private int branchNumber;
        private string branchAddress;
        private string branchCity;


        //Constructor
        public BankBranch() { }

        //Date fields
        public int BankNumber { get => bankNumber; set => bankNumber = value; }
        public string BankName { get => bankName; set => bankName = value; }
        public int BranchNumber { get => branchNumber; set => branchNumber = value; }
        public string BranchAddress { get => branchAddress; set => branchAddress = value; }
        public string BranchCity { get => branchCity; set => branchCity = value; }
        public override string ToString()
        {
            string str = "";
            str += "Bank number: " + BankNumber.ToString() + " Bank name: " + BankName + " Branch number: " + BankNumber.ToString() +
       " Branch address: " + BranchAddress + " Brach city: " + BranchCity+"\r\n";
            return str;
        }
    }
}
