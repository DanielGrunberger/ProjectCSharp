using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BE.Configuration;
using  BE;
using System.Xml.Serialization;
using Utilities;

namespace BE
{
   public class HostingUnit 
    {
        
        private int hostingUnitKey;
        private Host owner;
        private string hostingUnitName;
        private bool childrensAttractions;
        private bool pool;
        private bool jacuzzi;
        private bool garden;
        private Area area;
        private string subArea="none";
        private type type;
        private int adults;
        private int children;
        private int sum_to_pay = 0;
        private int busyDays;

        public int Adults { get => adults; set => adults = value; }
        public int Children { get => children; set => children = value; }
        public Area Area { get => area; set => area = value; }
        public string SubArea { get => subArea; set => subArea = value; }
        public type Type { get => type; set => type = value; }
        public int HostingUnitKey { get => hostingUnitKey; set => hostingUnitKey = value; }
        public Host Owner { get => owner; set => owner= value; }
        public string HostingUnitName { get => hostingUnitName; set => hostingUnitName = value; }
        public bool ChildrensAttractions { get => childrensAttractions; set => childrensAttractions = value; }
        public bool Pool { get => pool; set => pool= value; }
        public bool Jacuzzi { get => jacuzzi; set => jacuzzi= value; }
        public bool Garden { get => garden; set => garden = value; }
        public int Sum_to_pay { get => sum_to_pay; set => sum_to_pay=value ; }
        public int BusyDays { get => busyDays;  set => busyDays = GetAnnualBusyDays(); }

        // tell the XmlSerializer to ignore this Property.
        [XmlIgnore]
        public bool[,] Diary = new bool[12, 31];
        //optional. tell the XmlSerializer to name the Array Element as'Diary'
        // instead of 'DiaryDto'
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); } //5 is the number of roes in the matrix
        }




        //Functions
        public override string ToString()
        {
            string str = "";
            str +=  " Host: "  + Owner.PrivateName + "  " + Owner.FamilyName +
                " Hosting unit name: " +
                HostingUnitName +
                " Area:  "+
                Area.ToString()+
                " subArea: "+subArea+
                " Type: "+
                Type.ToString()+"\n";
            str += " Host details : " + owner.PrivateName + " " + 
                owner.FamilyName + " " 
                + owner.PhoneNumber + " " + owner.MailAddress+"\n";
                return str;





           
        }

        
        
        
        
        
        
        
        
        
        
        
        
      
        
        
        
        
        
        
        #region e
        public int GetAnnualBusyDays()
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 31; j++)
                {
                    if (Diary[i, j] == true)
                        sum++;
                }
            return sum;
        }
        public bool ApproveRequest(GuestRequest request)
        {
            if (request.Entry_Date.Year != 2020 || request.ReleaseDate.Year != 2020)
                return false;


            if (request.Area!=Area.All && this.area!=Area.All&&(!(request.Area == this.area && request.SubArea == this.subArea)))
                return false;

            //If the client wants something that the host doest not have
            if (request.Garden == Interested.Obligated)
                if (this.Garden == false)
                    return false;
            if (request.Jacuzzi == Interested.Obligated)
                if (this.jacuzzi == false)
                    return false;
            if (request.ChildrensAttractions == Interested.Obligated)
                if (this.ChildrensAttractions == false)
                    return false;
            if (request.Pool == Interested.Obligated)
                if (this.Pool == false)
                    return false;


            if (request.Type != this.type)
                return false;
            //If the client doest not want something that the host have
            if (request.Garden == Interested.Not_interested)
                if (this.Garden == true)
                    return false;
            if (request.Jacuzzi == Interested.Not_interested)
                if (this.jacuzzi == true)
                    return false;
            if (request.ChildrensAttractions == Interested.Not_interested)
                if (this.ChildrensAttractions == true)
                    return false;
            if (request.Pool == Interested.Not_interested)
                if (this.Pool == true)
                    return false;

            if (request.Adults > this.adults || request.Children > this.children)
                return false;
            //Chceking if the dates are not taken
            int entry_date_day = request.Entry_Date.Day;
            int entry_date_month = request.Entry_Date.Month;
            int release_date_day = request.ReleaseDate.Day;
            int release_date_month = request.ReleaseDate.Month;
            while ((entry_date_day != release_date_day + 1) || (entry_date_month != release_date_month))//Checking that we did all days on the request
            {
                if (this.Diary[entry_date_month - 1, entry_date_day - 1] == true)//if one day in between is taken , than we cannot take the request
                {
                    
                    return false;
                }
                if (entry_date_day == 31)
                {
                    entry_date_day = 1;
                    entry_date_month++;
                }
                if (entry_date_month == 13)//Is not on the same year
                {
                    throw new  InvalidInputException("The month input is not valid!");
                }
                else
                    entry_date_day++;
            }
            return true;
        }
        public bool ApproveRequest(DateTime entryDate,int duration)
        {
            int entry_date_day = entryDate.Day;
            int entry_date_month =entryDate.Month;
            for (int i=0; i<duration;i++)
            {
                if (Diary[entry_date_month - 1, entry_date_day - 1] == true)//if one day in between is taken , than we cannot take the request
                {

                    return false;
                }
                if (entry_date_day == 31)
                {
                    entry_date_day = 1;
                    entry_date_month++;
                }
                if (entry_date_month == 13)//Is not on the same year
                {
                    throw new InvalidInputException("The month input is not valid!");
                }
                else
                    entry_date_day++;
            }
            return true;
        }
        public void setRequest(GuestRequest guestRequest)
        {
            int entryDay = guestRequest.Entry_Date.Day;
            int entryMonth = guestRequest.Entry_Date.Month;
            int releaseDay = guestRequest.ReleaseDate.Day;
            int releaseMonth = guestRequest.ReleaseDate.Month;
            int days_In_The_Month = DateTime.DaysInMonth(2020, entryMonth);
            while (entryDay != releaseDay + 1 || entryMonth != releaseMonth)
            {
                this.Diary[entryMonth - 1,entryDay-1] = true;
                if (entryDay == days_In_The_Month)
                {
                    entryMonth++;
                    days_In_The_Month = DateTime.DaysInMonth(2020, entryMonth);
                    entryDay = 1;
                }
                else
                    entryDay++;
            }
        }
        #endregion


    }
}
