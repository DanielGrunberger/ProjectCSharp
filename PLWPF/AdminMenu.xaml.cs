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
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Show_Hosting_Button_Click(object sender, RoutedEventArgs e)
        {
            HostingUnitsList hostingUnitsListWindow = new HostingUnitsList();
            Close();
            hostingUnitsListWindow.ShowDialog();
        }

        private void Show_GuestRequest_Button_Click(object sender, RoutedEventArgs e)
        {
            GuestRequestList guestRequestListWindow = new GuestRequestList();
            Close();
            guestRequestListWindow.ShowDialog();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch(index)
            {
                case 0:
                    PrincipalGrid.Children.Clear();
                    PrincipalGrid.Children.Add(new SeeAllUnitsUserControl());
                    break;
                case 1:
                    PrincipalGrid.Children.Clear();
                    PrincipalGrid.Children.Add(new SeeRequestsUserControl());
                    break;
                case 2:
                    PrincipalGrid.Children.Clear();
                    PrincipalGrid.Children.Add(new SeeAllHostsUserControl());
                    break;

                case 3:
                    PrincipalGrid.Children.Clear();
                    PrincipalGrid.Children.Add(new SeeOrdersUserControl());
                    break;
            }
        }
    }
}
