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
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
         const string USERNAME = "Daniel";
        const string PASSWORD = "hello";
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string user = UsernameBox.Text;
            string pass = PasswordBox.Password.ToString();
            if (user==USERNAME && pass==PASSWORD)
            {
                Close();
                AdminMenu menuPage = new AdminMenu();
                menuPage.ShowDialog();
            
            }
            else
                MessageBox.Show("Incorret password and/or username !");
        }
    }
}
