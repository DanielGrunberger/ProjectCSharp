using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for Offers.xaml
    /// </summary>
    public partial class Offers : Window
    {
        private int current_guestRequest_Key = 0;
        public Offers(GuestRequest gr)
        {
            InitializeComponent();
            BL.IBL myBL = BlFactory.GetBL();
            current_guestRequest_Key = gr.GuestRequestKey;
            List<Order> order_options = myBL.send_Orders_To_Request(gr);
            if (order_options.Count == 0)
            {
                MessageBox.Show("No available options !", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach(Order ord in order_options)
            {
                HostingUnit unit = myBL.get_All_units().Find(x => x.HostingUnitKey == ord.HostingUnitKey);
                OrdersDataGrid.Items.Add(unit);

            }
        }

        private void reserve(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            MessageBoxResult result = MessageBox.Show("Are you sure you want to reserve this hosting unit ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                HostingUnit unit_chosen = OrdersDataGrid.SelectedItem as HostingUnit;      //Getting unit to update
                Order order_chosen = myBL.get_All_orders().Find(ord => ord.HostingUnitKey == unit_chosen.HostingUnitKey && ord.GuestRequestKey == current_guestRequest_Key);
                try
                {
                    order_chosen.Status = Order_Status.Mail_Sent;
                    myBL.Update_Order(order_chosen);
                }
                catch (KeyNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch(CantUpdateException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }




                MessageBoxResult boxResult = MessageBox.Show("\r We sent you an email with the details of the order", "Thank you for choosing us !", MessageBoxButton.OK, MessageBoxImage.Asterisk );
                if (boxResult == MessageBoxResult.OK)
                {
                    Close();
                }
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
