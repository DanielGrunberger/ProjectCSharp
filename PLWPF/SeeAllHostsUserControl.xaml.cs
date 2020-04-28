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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for SeeAllHostsUserControl.xaml
    /// </summary>
    public partial class SeeAllHostsUserControl : UserControl
    {
        public SeeAllHostsUserControl()
        {
            InitializeComponent();
            HostsDataGrid.Foreground = Brushes.Black;
            BL.IBL myBL = BlFactory.GetBL();
            List<Host> list = myBL.GetHosts();
            foreach(Host h in list)
            {
                HostsDataGrid.Items.Add(h);
            }
            //Erase extra row
            HostsDataGrid.IsReadOnly = true;
            this.HostsDataGrid.CanUserDeleteRows = true;
        }
        private void EraseHostBT(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this host ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Host temp = HostsDataGrid.SelectedItem as Host;
                myBL.erase_Host(temp);
                List<Host> list = myBL.GetHosts();
                HostsDataGrid.Items.Clear();
                foreach (Host host in list)
                {
                    HostsDataGrid.Items.Add(host);
                }
            }
          
        }
    }
}
