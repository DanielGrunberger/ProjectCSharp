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
    /// Interaction logic for SeeOrdersUserControl.xaml
    /// </summary>
    public partial class SeeOrdersUserControl : UserControl
    {
        public SeeOrdersUserControl()
        {
            InitializeComponent();
            AllOrderstDataGrid.Foreground = Brushes.Black;
            BL.IBL myBL = BlFactory.GetBL();
            List<Order> list = myBL.get_All_orders();
            foreach(Order o in list)
            {
                AllOrderstDataGrid.Items.Add(o);
            }

            //Erase extra host
            AllOrderstDataGrid.IsReadOnly = true;
            this.AllOrderstDataGrid.CanUserDeleteRows = true;
        }
    }
}
