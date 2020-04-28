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
    /// Interaction logic for SeeUnitsWindow.xaml
    /// </summary>
    public partial class SeeUnitsWindow : Window
    {
        private Host currentHost = new Host();
        public SeeUnitsWindow(BE.Host currentHost)
        {
            this.currentHost = currentHost;
            this.WindowState = WindowState.Maximized;
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            UnitsToEraseDataGrid.Foreground = Brushes.Black;
            List<HostingUnit> list = myBL.units_of_Host(currentHost);
            foreach (HostingUnit unit in list)
            {
                unit.BusyDays = unit.GetAnnualBusyDays();
                UnitsToEraseDataGrid.Items.Add(unit);
            }
            //Erase extra row
            UnitsToEraseDataGrid.IsReadOnly = true;
            this.UnitsToEraseDataGrid.CanUserDeleteRows = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
