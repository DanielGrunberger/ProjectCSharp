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
    /// Interaction logic for UpdateUnitWindow.xaml
    /// </summary>
    public partial class UpdateUnitWindow : Window
    {
        private Host currentHost = new Host();
        public UpdateUnitWindow(BE.Host currentHost)
        {
            this.currentHost = currentHost;
            this.WindowState = WindowState.Maximized;
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            HostUnitsDataGrid.Foreground = Brushes.Black;
            List<HostingUnit> list = myBL.units_of_Host(currentHost);
            foreach (HostingUnit unit in list)
            {
                HostUnitsDataGrid.Items.Add(unit);
            }
            //Erase extra row
            HostUnitsDataGrid.IsReadOnly = true;
            this.HostUnitsDataGrid.CanUserDeleteRows = true;
        }


        private void UpdateHostingUnitBT(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            HostingUnit temp = HostUnitsDataGrid.SelectedItem as HostingUnit;
            UnitToUpdateWindow unitToUpdateWindow = new UnitToUpdateWindow(temp);
            unitToUpdateWindow.ShowDialog();
            HostUnitsDataGrid.Items.Clear();
            List<HostingUnit> list = myBL.units_of_Host(currentHost);
            foreach (HostingUnit unit in list)
            {
                HostUnitsDataGrid.Items.Add(unit);
            }
    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
