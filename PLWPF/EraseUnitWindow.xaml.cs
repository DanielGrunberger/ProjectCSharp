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
    /// Interaction logic for EraseUnitWindow.xaml
    /// </summary>
    public partial class EraseUnitWindow : Window
    {
        private Host currentHost = new Host();
        public EraseUnitWindow(BE.Host currentHost)
        {
            this.currentHost = currentHost;
            this.WindowState = WindowState.Maximized;
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            UnitsToEraseDataGrid.Foreground = Brushes.Black;
            List<HostingUnit> list = myBL.units_of_Host(currentHost);
            foreach (HostingUnit unit in list)
            {
                UnitsToEraseDataGrid.Items.Add(unit);
            }
            //Erase extra row
            UnitsToEraseDataGrid.IsReadOnly = true;
            this.UnitsToEraseDataGrid.CanUserDeleteRows = true;
        }
        private void EraseHostingUnitBT(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this hosting unit ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HostingUnit temp = UnitsToEraseDataGrid.SelectedItem as HostingUnit;//Getting unit to delete
                try
                {
                    myBL.Erase_Hosting_Unit(temp);//Deleting unit
                }
                catch (Can_tEraseException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (KeyNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //Loading new list on data grid
                UnitsToEraseDataGrid.Items.Clear();
                List<HostingUnit> list = myBL.units_of_Host(currentHost);
                foreach (HostingUnit unit in list)
                {
                    UnitsToEraseDataGrid.Items.Add(unit);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
