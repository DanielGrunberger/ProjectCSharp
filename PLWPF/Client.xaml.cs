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
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRequest addRequest = new AddRequest();
            addRequest.ShowDialog();
        }

        private void Update_Click (object sender, RoutedEventArgs e)
        {
            Update_Guestrequest update = new Update_Guestrequest();
            update.ShowDialog();
        }

        private void MyRequest_Click_1(object sender, RoutedEventArgs e)
        {
            SeeYourGuestrequest Request = new SeeYourGuestrequest();
            Request.ShowDialog();
        }
    }
}
