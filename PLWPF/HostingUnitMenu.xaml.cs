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
    /// Interaction logic for HostingUnitMenu.xaml
    /// </summary>
    public partial class HostingUnitMenu : Window
    {
        private Host currentHost=new Host();

        public HostingUnitMenu(Host currentHost)
        {
            BL.IBL myBL = BlFactory.GetBL();
            InitializeComponent();
            this.currentHost = currentHost;
            HostNameLabel.Content = "Welcome " + currentHost.PrivateName + "!";

        }

        private void AddUnitButton_Click(object sender, RoutedEventArgs e)
        {
            AddUnitWindow addUnitWindow = new AddUnitWindow(this.currentHost);
            addUnitWindow.ShowDialog();

        }

        private void UpdateUnitButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateUnitWindow updateUnitWindow = new UpdateUnitWindow(this.currentHost);
            updateUnitWindow.ShowDialog();
        }

        private void EraseUnitButton_Click(object sender, RoutedEventArgs e)
        {
            EraseUnitWindow eraseUnitWindow = new EraseUnitWindow(this.currentHost);
            eraseUnitWindow.ShowDialog();
        }

        private void SeeUnitsButton_Click(object sender, RoutedEventArgs e)
        {
            SeeUnitsWindow seeUnitsWindow = new SeeUnitsWindow(this.currentHost);
            seeUnitsWindow.ShowDialog();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            BL.IBL myBL = BlFactory.GetBL();
            this.currentHost = myBL.GetHosts().Find(x => x.HostKey == currentHost.HostKey);
            ProfileWindow profileWindow = new ProfileWindow(currentHost);
            profileWindow.ShowDialog();
        }

        private void SeeOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            HostOrdersWindow hostOrdersWindow = new HostOrdersWindow(this.currentHost);
            hostOrdersWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RatingWindow ratingWindow = new RatingWindow(currentHost);
            ratingWindow.ShowDialog();
        }
    }
}
