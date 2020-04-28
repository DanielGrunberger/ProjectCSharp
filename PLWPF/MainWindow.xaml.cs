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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            BL.IBL myBL = BlFactory.GetBL();
            myBL.setRequests();
            InitializeComponent();
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
           
            AdminLogin adminPage = new AdminLogin();
            adminPage.ShowDialog();
        }

        private void HostButton_Click(object sender, RoutedEventArgs e)
        {
            HostMenu hostPage = new HostMenu();
            hostPage.ShowDialog();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            Client clientWindow = new Client();
            clientWindow.ShowDialog();
        }

       
    }
}
