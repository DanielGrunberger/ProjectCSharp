using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for SeeYourGuestrequest.xaml
    /// </summary>
    public partial class SeeYourGuestrequest : Window
    {
   
            BL.IBL bL = BlFactory.GetBL();
            private GuestRequest current_questRequest;

            public SeeYourGuestrequest()
            {
                InitializeComponent();
                current_questRequest = new GuestRequest();
                MyREquestGrid.DataContext = current_questRequest;

            //make all filed inactive
            p_name.IsEnabled = false;
            f_name.IsEnabled = false;
            id.IsEnabled = false;
            mail.IsEnabled = false;
            CBType.IsEnabled = false;
            CBType.IsEnabled = false;
            CBPool.IsEnabled = false;
            CBJacuzzi.IsEnabled = false;
            CBGarden.IsEnabled = false;
            CBChildAttraction.IsEnabled = false;
            subArea.IsEnabled = false;
            children.IsEnabled = false;
            adults.IsEnabled = false;
            entry_date.IsEnabled = false;
            release_date.IsEnabled = false;

             
            CBArea.ItemsSource = Enum.GetValues(typeof(Area)).Cast<Area>();
            CBType.ItemsSource = Enum.GetValues(typeof(type)).Cast<type>();
            CBPool.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBJacuzzi.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBGarden.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();
            CBChildAttraction.ItemsSource = Enum.GetValues(typeof(Interested)).Cast<Interested>();

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
            MyREquestGrid.DataContext = current_questRequest;
            entry_date.SelectedDate = current_questRequest.Entry_Date;
            release_date.SelectedDate = current_questRequest.ReleaseDate;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
