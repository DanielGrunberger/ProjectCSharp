using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BE
{
    public class GuestRequest
    {
        private int guestRequestKey=0;
        private string privateName;
        private string familyName;
       
        private Guest_Request_Status status;
        private Area area;
        private string subArea="none";
        private type type;
        private int adults;
        private int children;
        private Interested pool;
        private Interested jacuzzi;
        private Interested garden;
        private Interested childrensAttractions;
        private DateTime registrationDate = new DateTime();
        private DateTime entry_Date = new DateTime();
        private DateTime releaseDate = new DateTime();
        private int id;
        //Constructor
        public GuestRequest() { }
       

 
        public int ID { get => id; set => id = value; }
        public int GuestRequestKey { get => guestRequestKey; set => guestRequestKey = value; }
        public string PrivateName { get => privateName; set => privateName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }

        public Guest_Request_Status Status { get => status; set => status = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
       public DateTime Entry_Date { get => entry_Date; set => entry_Date = value; }
        public Area Area { get => area; set => area = value; }
        public string SubArea { get => subArea; set => subArea = value; }
        public type Type { get => type; set => type = value; }
        public int Adults { get => adults; set => adults = value; }
        public int Children { get => children; set => children = value; }
        public Interested Pool { get => pool; set => pool = value; }
        public Interested Jacuzzi { get => jacuzzi; set => jacuzzi = value; }
        public Interested Garden { get => garden; set => garden = value; }
        public Interested ChildrensAttractions { get => childrensAttractions; set => childrensAttractions = value; }
        public string MailAddress { get; set; }

        public override string ToString()
        {
            string str = "";
            str += "Key: " + GuestRequestKey.ToString() +" ID:  "+ID+ " Private name: " + PrivateName + " Family name: " + FamilyName + " Mail address: " + MailAddress +
                " Registration Date: " + RegistrationDate.ToShortDateString()+" Entry date:  "+entry_Date.ToShortDateString() + " Release date: " + ReleaseDate.ToShortDateString() + " Area: " + area.ToString() +
                " SubArea: " + SubArea + " type: " + type.ToString() + " Numbers of adults: " + Adults.ToString() + " Number of children: " + Children.ToString() +
                " Interested in: " + " Pool: "+Pool.ToString() + " Jacuzzi:  " + Jacuzzi.ToString() + " Garden:  " + Garden.ToString() + " Children Attractions:  " + ChildrensAttractions.ToString() + "\r\n";
            return str;
        }
    }
}
