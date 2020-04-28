using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Window
    {
        private GuestRequest new_guestRequest;
        BL.IBL bL = BlFactory.GetBL();
        public AddRequest()
        {
            InitializeComponent();
           
            new_guestRequest = new GuestRequest();
            GuestReguestGrid.DataContext = new_guestRequest;

            CBArea.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>();
            CBType.ItemsSource = Enum.GetValues(typeof(type)).Cast<type>();
            CBPool.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBJacuzzi.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBGarden.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBChildAttraction.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            entry_date.DisplayDateStart = DateTime.Now;
            entry_date.DisplayDateEnd = DateTime.Now.AddMonths(11);
            release_date.DisplayDateStart = DateTime.Now;
            release_date.DisplayDateEnd = DateTime.Now.AddMonths(11);

        }
        //  function to check if mailaddress is currect
        public static bool ValidateMail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (p_name.Text == "" ||
                    f_name.Text == "" ||
                    id.Text == "" ||
                    mail.Text == "" ||
                    entry_date.Text.ToString() == "" ||
                    release_date.Text.ToString() == "" ||
                    adults.Text == "" ||
                    children.Text == "" ||
                    CBArea.Text == "" ||
                    CBType.Text == "" ||
                    CBPool.Text == "" ||
                    CBJacuzzi.Text == "" ||
                    CBGarden.Text == "" ||
                    CBChildAttraction.Text == "")
                {
                    throw new InvalidInputException("One or more inputs are invalid ! ");
                }

                
                // casting of string to int in id
                if (!int.TryParse(id.Text, out int client_id) || id.Text.Length != 9)
                {
                    throw new InvalidInputException("wrong id , Please enter a 9-digit ID ! ");
                }
               

                // check validate of mail
                if (!ValidateMail(mail.Text))
                {
                    throw new InvalidInputException("wrong mail address ! ");
                }

                new_guestRequest.RegistrationDate = DateTime.Now;

                new_guestRequest.Entry_Date = (DateTime)entry_date.SelectedDate;
                new_guestRequest.ReleaseDate = (DateTime)release_date.SelectedDate;

                // check the entered date
                if (new_guestRequest.Entry_Date > new_guestRequest.ReleaseDate)
                {
                    throw new InvalidInputException("Please enter date bigger than reslease date ! ");
                }

                //// casting of string to int in adults
                if (!int.TryParse(adults.Text, out int numOfAdults) || int.Parse(adults.Text) <= 0)
                {
                    throw new InvalidInputException("wrong num of adults ! ");
                }


                //  casting of string to int in children
                if (!int.TryParse(children.Text, out int numOfChildren) || int.Parse(adults.Text) < 0)
                {
                    throw new InvalidInputException("wrong num of children ! ");
                }
                new_guestRequest.ID = int.Parse(id.Text);
                new_guestRequest.Area = (Area)Enum.Parse(typeof(Area), CBArea.Text, true);
                new_guestRequest.Type = (type)Enum.Parse(typeof(type), CBType.Text, true);
                new_guestRequest.Pool = (Interested)Enum.Parse(typeof(Interested), CBPool.Text, true);
                new_guestRequest.Jacuzzi = (Interested)Enum.Parse(typeof(Interested), CBJacuzzi.Text, true);
                new_guestRequest.Garden = (Interested)Enum.Parse(typeof(Interested), CBGarden.Text, true);
                new_guestRequest.ChildrensAttractions = (Interested)Enum.Parse(typeof(Interested), CBChildAttraction.Text, true);
                new_guestRequest.MailAddress = mail.Text;
                new_guestRequest.Adults = int.Parse(adults.Text);
                new_guestRequest.Children = int.Parse(children.Text);
             
                bL.Add_Guest_Request(new_guestRequest);
                new_guestRequest.GuestRequestKey = bL.get_All_Requests().Find(x => x.ID == new_guestRequest.ID).GuestRequestKey;
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Offers offer = new Offers(new_guestRequest);

            offer.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Adults_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (adults.Text!="")
            AdultSlider.Value = int.Parse(adults.Text);
        }

        private void Children_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(children.Text!="")
            ChildrenSlider.Value = int.Parse(children.Text);
        }
    }
}
