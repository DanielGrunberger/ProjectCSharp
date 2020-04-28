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
    /// Interaction logic for AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        private Host currentHost;

        public AddUnitWindow(Host CurrentHost)
        {
            this.currentHost = CurrentHost;
            InitializeComponent();
        }

        private void AdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AdultTextBox.Text != "")
                try 
                {
                    AdultSlider.Value = int.Parse(AdultTextBox.Text);
                }
                catch(Exception ex)
                {

                }
        }

        private void ChildrenTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ChildrenTextBox.Text != "")
                try
                {
                    ChildrenSlider.Value = int.Parse(ChildrenTextBox.Text);
                }
                catch (Exception ex)
                {

                }
        }

        private void HostingUnitNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (HostingUnitNameTextBox.Text == ""|| AreaComboBox.SelectedItem == null|| SubAreaTextBox.Text == "")
            {
                if (HostingUnitNameTextBox.Text == "")
                {
                    HostingUnitNameLabel.Foreground = Brushes.Red;
                    HostingUnitNameLabel.Content = "Empty field !";
                }
                else
                    HostingUnitNameLabel.Content = "";
                if (AreaComboBox.SelectedItem == null)
                {
                    AreaLabel.Foreground = Brushes.Red;
                    AreaLabel.Content = "Empty field !";
                }
                if(TypeComboBox.SelectedItem==null)
                {
                    TypeLabel.Foreground = Brushes.Red;
                    TypeLabel.Content = "Empty field !";
                }
             
                
                MessageBox.Show("Please fill all the fields !", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //If he filled all the details
            else
            {
                BL.IBL myBL = BlFactory.GetBL();
                HostingUnit newHostingUnit = new HostingUnit();
                newHostingUnit.HostingUnitName = HostingUnitNameTextBox.Text;
                newHostingUnit.Owner = currentHost;
                newHostingUnit.Adults = int.Parse(AdultTextBox.Text);
                newHostingUnit.Area = (Area)AreaComboBox.SelectedItem;
                newHostingUnit.Children =int.Parse(ChildrenTextBox.Text);
                newHostingUnit.ChildrensAttractions = (bool)AttractionCheckBox.IsChecked;
                newHostingUnit.Garden= (bool)GardenCheckBox.IsChecked;
                newHostingUnit.Jacuzzi = (bool)JacuzziCheckBox.IsChecked;
                newHostingUnit.Pool = (bool)PoolCheckBox.IsChecked;
                myBL.Add_Hosting_Unit(newHostingUnit);
                MessageBox.Show("Added with success!", "Thank you !", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Close();
               
            }
        }

      
    }
}
