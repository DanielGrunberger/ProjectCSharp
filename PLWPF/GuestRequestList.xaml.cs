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
    /// Interaction logic for GuestRequestList.xaml
    /// </summary>
    public partial class GuestRequestList : Window
    {
        public GuestRequestList()
        {
            this.WindowState = WindowState.Maximized;//Setting window size to maximum
            InitializeComponent();
            BL.IBL myBL = BlFactory.GetBL();
            List<GuestRequest> list = myBL.get_All_Requests();
            foreach (GuestRequest unit in list)
            {
                GuestRequestDataGrid.Items.Add(unit);
            }
            //Erase extra row
            GuestRequestDataGrid.IsReadOnly = true;
            this.GuestRequestDataGrid.CanUserDeleteRows = true;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
