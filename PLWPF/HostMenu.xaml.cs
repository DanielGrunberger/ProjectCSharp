using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for HostMenu.xaml
    /// </summary>
    public partial class HostMenu : Window
    {
        public HostMenu()
        {
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            try
            {
                LoadBanks(myBL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoginButton.MouseEnter += LoginButton_MouseEnter;
            LoginButton.MouseLeave += LoginButton_MouseLeave;
            SignInButton.MouseEnter += SignInButton_MouseEnter;
            SignInButton.MouseLeave += SignInButton_MouseLeave;
        }

        private void SignInButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SignInButton.Width = 210;
            SignInButton.Height = 39;
        }

        private void SignInButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SignInButton.Width = 230;
            SignInButton.Height = 50;
        }

        private void LoginButton_MouseLeave(object sender, MouseEventArgs e)
        {
            LoginButton.Width = 204;
            LoginButton.Height = 39;
        }

        private void LoginButton_MouseEnter(object sender, MouseEventArgs e)
        {
            LoginButton.Width = 230;
            LoginButton.Height = 50;
        }

        private void LoadBanks(IBL myBL)
        {
            foreach (var bank in myBL.get_Bank_List())
            {
                BankNameComboBox.Items.Add(bank.BankName);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            //Verifying that the user filed all details
            if(ClearanceComboBox.SelectedItem==null)
            {
                ClearanceLabel.Foreground = Brushes.Red;
                ClearanceLabel.Content = "Empty field !";
            }
            else
            {
                ClearanceLabel.Content = "";
            }
            if (NameTextBox.Text == "" || LastnameTextBox.Text == "" || MailTextBox.Text == "" || BankAccountTextBox.Text == ""||
                BranchNumberTextBox.Text == "" || BranchCityTextBox.Text == "" || BranchAddressTextBox.Text == "")
            {
                if(BankNameComboBox.SelectedItem==null)

                {
                    BankNameLabel.Foreground = Brushes.Red;
                    BankNameLabel.Content = "Empty field !";
                }
                else
                {
                    BankNameLabel.Content = "";
                }

                    if (NameTextBox.Text == "")
                    {
                        NameLabel.Foreground = Brushes.Red;
                        NameLabel.Content = "Empty field !";
                    }
                    else
                {
                    NameLabel.Content = "";
                }

                if (BranchAddressTextBox.Text == "")
                {
                    BranchAddLabel.Foreground = Brushes.Red;
                    BranchAddLabel.Content = "Empty field !";
                }
                else
                {
                    BranchAddLabel.Content = "";

                }
 
                if (LastnameTextBox.Text == "")
                {
                    LastNameLabel.Foreground = Brushes.Red;
                }
               
                    if (PhoneTextBox.Text == "")
                    {
                        PhoneLabel.Foreground = Brushes.Red;
                        PhoneLabel.Content = "Empty field !";
                   
                }
               
                    if (UsernameTextBox.Text == "")
                    {
                        UsernameLabel.Foreground = Brushes.Red;
                        UsernameLabel.Content = "Empty field !";
                    }

                   
              
                    if (BankAccountTextBox.Text == "")
                    {
                        BankAccLabel.Foreground = Brushes.Red;
                        BankAccLabel.Content = "Empty field !";
                    }

                if (LastnameTextBox.Text == "")
                {
                    LastNameLabel.Foreground = Brushes.Red;
                    LastNameLabel.Content = "Empty field !";
                }


                if (BankNumberTextBox.Text == "")
                {
                    BankNumLabel.Foreground = Brushes.Red;
                    BankNumLabel.Content = "Empty field !";

                }

                    if (BranchCityTextBox.Text == "")
                    {
                        BranchCityLabel.Foreground = Brushes.Red;
                        BranchCityLabel.Content = "Empty field !";
                    }



                    if (BranchNumberTextBox.Text == "")
                    {
                        BranchNumLabel.Foreground = Brushes.Red;
                    BranchNumLabel.Content = "Empty field !";

                }
                if(PasswordTextBox.Text=="")
                {
                    PasswordLabel.Foreground = Brushes.Red;
                    PasswordLabel.Content = "Empty field !";
                }
                if (MailTextBox.Text == "")
                {
                    MailLabel.Foreground = Brushes.Red;
                    MailLabel.Content = "Empty field !";
                }




                MessageBox.Show("Please fill all the fields !", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else
            {
               
                Host new_Host = new Host();
                new_Host.PrivateName = NameTextBox.Text;
                new_Host.FamilyName = LastnameTextBox.Text;
                try
                {
                    new_Host.BankAccountNumber = int.Parse(BankAccountTextBox.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                try
                {
                    MailAddress mailAddress = new MailAddress(MailTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid mail address!", "ERROR");
                    return;
                }
                new_Host.MailAddress = MailTextBox.Text;
                try
                {
                    new_Host.PhoneNumber = int.Parse(PhoneTextBox.Text);
                }
               
                 catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                
                }
               
                new_Host.BankBranchDetails = new BankBranch()
                {
                    BankName = (string)BankNameComboBox.SelectedItem,
                    BankNumber =int.Parse(BankNumberTextBox.Text),
                    BranchCity = BranchCityTextBox.Text,
                    BranchAddress = BranchAddressTextBox.Text,
                    BranchNumber = int.Parse(BranchNumberTextBox.Text)
                };
                new_Host.Username = UsernameTextBox.Text;
                new_Host.Password = PasswordTextBox.Text;

                myBL.Add_Host(new_Host);
                MessageBox.Show("Signed up with success!", "Welcome", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                ClearTextBoxes();
                BankAccLabel.Content = "";
                BankNameLabel.Content = "";
                BankNumLabel.Content = "";
                BranchAddLabel.Content = "";
                UsernameLabel.Content = "";
                PhoneLabel.Content = "";
                PasswordLabel.Content = "";
                PassswordLabel.Content = "";
                NameLabel.Content = "";
                MailLabel.Content = "";
                LastNameLabel.Content = "";
                BranchNumLabel.Content = "";
                BranchCityLabel.Content = "";

            }

        }

        //Function to clear all text boxes
        private void ClearTextBoxes()
        {
            BankAccountTextBox.Text = String.Empty;
            BankNameComboBox.SelectedItem = null;
            NameTextBox.Text = String.Empty;
            LastnameTextBox.Text = String.Empty;
            MailTextBox.Text = String.Empty;
            PhoneTextBox.Text = String.Empty;
            BankNumberTextBox.Text = String.Empty;
            BranchCityTextBox.Text = String.Empty;
            BranchAddressTextBox.Text = String.Empty;
            BranchNumberTextBox.Text = String.Empty;
            UsernameTextBox.Text = String.Empty;
            PasswordTextBox.Text = String.Empty;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HostLogin hostLoginWindow = new HostLogin();
            hostLoginWindow.Show();
            this.Close();
        }

        private void MailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MailTextBox.Text == "")
            {
               MailLabel.Foreground = Brushes.Red;
                MailLabel.Content = "Empty field !";
            }
            try
            {
                MailAddress mailAddress = new MailAddress(MailTextBox.Text);
            }
            catch(Exception ex)
            {
                MailLabel.Foreground = Brushes.Red;
                MailLabel.Content = "Invalid mail address !";
                return;
            }
            MailLabel.Foreground = Brushes.Green;
            MailLabel.Content = "Valid mail address !";

        }

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordTextBox.Text == "")
            {
                PasswordLabel.Foreground = Brushes.Red;
                PasswordLabel.Content = "Empty field !";
            }
            if (PasswordTextBox.Text.Length <5)
            {
                PasswordLabel.Foreground = Brushes.Red;
                PasswordLabel.Content = "At least 5 characters !";
            }
            else
            {
                PasswordLabel.Foreground = Brushes.Green;
                PasswordLabel.Content = "Valid password !";
            }
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameTextBox.Text == "")
            {
                NameLabel.Foreground = Brushes.Red;
                NameLabel.Content = "Empty field !";
            }
            else
            {
                NameLabel.Content = "";
            }

            }

        private void LastnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LastnameTextBox.Text == "")
            {
                LastNameLabel.Foreground = Brushes.Red;
                LastNameLabel.Content = "Empty field !";
            }
            else
            {
                LastNameLabel.Content = "";
            }
        }
        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (PhoneTextBox.Text == "")
                {
                    PhoneLabel.Foreground = Brushes.Red;
                    PhoneLabel.Content = "Empty field !";
                }
                else
                {
                PhoneLabel.Content = "";
                }
            }
        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (UsernameTextBox.Text == "")
                {
                    UsernameLabel.Foreground = Brushes.Red;
                    UsernameLabel.Content = "Empty field !";
                }

                else
                {
                UsernameLabel.Content = "";
                }
            }
        private void BankAccountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BankAccountTextBox.Text == "")
            {
                BankAccLabel.Foreground = Brushes.Red;
              BankAccLabel.Content = "Empty field !";
            }

            else
            {
                BankAccLabel.Content = "";
            }
        }

       



        private void BankNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BankNameComboBox.SelectedItem == null)

            {
                BankNameLabel.Foreground = Brushes.Red;
                BankNameLabel.Content = "Empty field !";
            }
            else
            {
                BankNameLabel.Content = "";

                BL.IBL myBL = BlFactory.GetBL();
                BankNumberTextBox.Text = myBL.get_Bank_List().Find(x => x.BankName == (string)BankNameComboBox.SelectedItem).BankNumber.ToString();
                BranchCityTextBox.Text = myBL.get_Bank_List().Find(x => x.BankName == (string)BankNameComboBox.SelectedItem).BranchCity.ToString();
                BranchAddressTextBox.Text = myBL.get_Bank_List().Find(x => x.BankName == (string)BankNameComboBox.SelectedItem).BranchAddress.ToString();
                BranchNumberTextBox.Text = myBL.get_Bank_List().Find(x => x.BankName == (string)BankNameComboBox.SelectedItem).BranchNumber.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
