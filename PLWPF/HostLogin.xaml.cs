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
    /// Interaction logic for HostLogin.xaml
    /// </summary>
    public partial class HostLogin : Window
    {
        public HostLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            string user = UsernameBox.Text;
            string pass = PasswordBox.Password.ToString();
            foreach(Host host in myBL.GetHosts())
            {
                if (host.Username==user && host.Password==pass)
                {
                    Host currentHost = myBL.GetHosts().Find(x => x.Username == user && x.Password == pass);
                    HostingUnitMenu hostingUnitMenuWindow = new HostingUnitMenu(currentHost);
                    this.Close();
                    hostingUnitMenuWindow.ShowDialog();
                    
                    return;
                }
            }
            MessageBox.Show("Username and/or password incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
