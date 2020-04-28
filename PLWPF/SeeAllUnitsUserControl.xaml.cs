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
    /// Interaction logic for SeeAllUnitsUserControl.xaml
    /// </summary>
    public partial class SeeAllUnitsUserControl : UserControl
    {
        public SeeAllUnitsUserControl()
        {
            InitializeComponent();
            //HostingUnitDataGrid = new DataGrid();
            HostingUnitDataGrid.Foreground = Brushes.Black;
            BL.IBL myBL = BlFactory.GetBL();
            List<HostingUnit> list = myBL.get_All_units();
            foreach (HostingUnit unit in list)
            {
                HostingUnitDataGrid.Items.Add(unit);
            }
            //Erase extra row
            HostingUnitDataGrid.IsReadOnly = true;
            this.HostingUnitDataGrid.CanUserDeleteRows = true;

        }
        private void EraseHostingUnitBT(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this hosting unit ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HostingUnit temp = HostingUnitDataGrid.SelectedItem as HostingUnit;//Getting unit to delete
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
                HostingUnitDataGrid.Items.Clear();
                List<HostingUnit> list = myBL.get_All_units(); 
                foreach (HostingUnit unit in list)
                {
                    HostingUnitDataGrid.Items.Add(unit);
                }




            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            HostingUnitDataGrid.Items.Clear();
            IEnumerable<IGrouping<Area, HostingUnit>> list = myBL.HostingUnits_On_This_Area();
            IEnumerable<HostingUnit> newList = list.SelectMany(group => group);
            List<HostingUnit> listToShow = newList.ToList();
            foreach(HostingUnit unit in listToShow)
            {
                HostingUnitDataGrid.Items.Add(unit);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            HostingUnitDataGrid.Items.Clear();
            
        }
    }
    }
