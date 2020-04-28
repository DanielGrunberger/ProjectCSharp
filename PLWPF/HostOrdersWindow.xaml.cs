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
    /// Interaction logic for HostOrdersWindow.xaml
    /// </summary>
    public partial class HostOrdersWindow : Window
    {
  
        private Host currentHost = new Host();
        public HostOrdersWindow(Host currentHost)
        {
            this.currentHost = currentHost;
            this.WindowState = WindowState.Maximized;
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            List<Order> ordersList = myBL.Orders_Sent_To_Host(currentHost);
            foreach(Order o in ordersList)
            {
                OrdersToHostDataGrid.Items.Add(o);
            }
            //Erase extra row
            OrdersToHostDataGrid.IsReadOnly = true;
            this.OrdersToHostDataGrid.CanUserDeleteRows = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
