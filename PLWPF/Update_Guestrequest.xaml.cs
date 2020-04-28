using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Update_Guestrequest.xaml
    /// </summary>
    public partial class Update_Guestrequest : Window
    {
        BL.IBL bL = BlFactory.GetBL();
        private GuestRequest current_questRequest;

        public Update_Guestrequest()
        {
            InitializeComponent();
            current_questRequest = new GuestRequest();
            UpdateGrid.DataContext = current_questRequest;

            //make all filed inactive before inserting ID
            for (int i = 0; i < UpdateGrid.Children.Count; i++)
            {
                if (UpdateGrid.Children[i] != current_id && UpdateGrid.Children[i] != Check && UpdateGrid.Children[i] != Cancel && UpdateGrid.Children[i] !=firstLable)
                {
                    UpdateGrid.Children[i].IsEnabled = false;
                    UpdateGrid.Children[i].Opacity = 0.5;
                }
            }
           
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
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if not entered id
                if (current_id.Text == "")
                {
                    throw new InvalidInputException("No id entered ! ");
                }

                // casting of string to int in id
                if (!int.TryParse(current_id.Text, out int id) || current_id.Text.Length != 9)
                {
                    throw new InvalidInputException("wrong id , Please enter a 9-digit ID ! ");
                }
                // find the questRequest related to id that we received
                current_questRequest = bL.get_All_Requests().Find(gr => gr.ID == Int32.Parse(current_id.Text));
                // if no questRequest exist with id that we received
                if (current_questRequest == null)
                {
                    throw new RequestNotFoundException("There is no guestrequest with this id !");
                }
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (RequestNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // update all filed
            UpdateGrid.DataContext = current_questRequest;
            entry_date.SelectedDate = current_questRequest.Entry_Date;
            release_date.SelectedDate = current_questRequest.ReleaseDate;

            //make all filed active last inserting correct ID apart from private & family name & id
            for (int i = 0; i < UpdateGrid.Children.Count; i++)
            {
                if (UpdateGrid.Children[i] != current_id && UpdateGrid.Children[i] != Check && UpdateGrid.Children[i] != p_name && UpdateGrid.Children[i] != f_name && UpdateGrid.Children[i] != id)
                {
                    UpdateGrid.Children[i].IsEnabled = true;
                    UpdateGrid.Children[i].Opacity = 0.6;
                }
                else
                {
                    UpdateGrid.Children[i].IsEnabled = false;
                    UpdateGrid.Children[i].Opacity = 0.5;
                }
            }
        }

        //  function to check if mail address is correct
        public static bool ValidateMail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
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

                current_questRequest.RegistrationDate = DateTime.Now;

                current_questRequest.Entry_Date = (DateTime)entry_date.SelectedDate;
                current_questRequest.ReleaseDate = (DateTime)release_date.SelectedDate;

                // check the entered date
                if (current_questRequest.Entry_Date > current_questRequest.ReleaseDate)
                {
                    throw new InvalidInputException("Please enter date bigger than release date ! ");
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
                current_questRequest.Children = numOfChildren;


                current_questRequest.Area = (Area)Enum.Parse(typeof(Area), CBArea.Text, true);
                current_questRequest.Type = (type)Enum.Parse(typeof(type), CBType.Text, true);
                current_questRequest.Pool = (Interested)Enum.Parse(typeof(Interested), CBPool.Text, true);
                current_questRequest.Jacuzzi = (Interested)Enum.Parse(typeof(Interested), CBJacuzzi.Text, true);
                current_questRequest.Garden = (Interested)Enum.Parse(typeof(Interested), CBGarden.Text, true);
                current_questRequest.ChildrensAttractions = (Interested)Enum.Parse(typeof(Interested), CBChildAttraction.Text, true);

                bL.Update_Guest_Request(current_questRequest);
                current_questRequest.GuestRequestKey = bL.get_All_Requests().Find(x => x.ID == current_questRequest.ID).GuestRequestKey;
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
            Offers offer = new Offers(current_questRequest);

            offer.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

     
    }
}
