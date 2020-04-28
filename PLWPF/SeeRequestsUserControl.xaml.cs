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
    /// Interaction logic for SeeRequestsUserControl.xaml
    /// </summary>
    public partial class SeeRequestsUserControl : UserControl
    {
        public SeeRequestsUserControl()
        {
            InitializeComponent();
            GuestRequestDataGrid.Foreground = Brushes.Black;
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
    }
}
