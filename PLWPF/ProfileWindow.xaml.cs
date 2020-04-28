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
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private Host currentHost = new Host();
        public ProfileWindow(Host currentHost)
        {
         
            InitializeComponent();
            this.currentHost = currentHost;
            NameLabel.Text = currentHost.PrivateName;
            LastnameLabel.Text = currentHost.FamilyName;
            MailTextBox.Text = currentHost.MailAddress;
            PhoneTextBox.Text = currentHost.PhoneNumber.ToString();
            UsernameTextBlock.Text = currentHost.Username;
            PasswordTextBlock.Text = currentHost.Password;
            UnitsTextBlock.Text = currentHost.NumOfUnits.ToString();
            ClearanceComboBox.SelectedItem = currentHost.CollectionClearance;

        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            currentHost.MailAddress = MailTextBox.Text;
            currentHost.PhoneNumber = int.Parse(PhoneTextBox.Text);
            currentHost.CollectionClearance = (Clearance)ClearanceComboBox.SelectedItem;
            myBL.Update_Host(this.currentHost);
            MessageBox.Show("Updated with success !", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
