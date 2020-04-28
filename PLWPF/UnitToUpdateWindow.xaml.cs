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
    /// Interaction logic for UnitToUpdateWindow.xaml
    /// </summary>
    public partial class UnitToUpdateWindow : Window
    {
        private HostingUnit updateUnit=new HostingUnit();
        public UnitToUpdateWindow(HostingUnit currentUnit)
        {


            BL.IBL myBL = BlFactory.GetBL();
            this.updateUnit = currentUnit;
            InitializeComponent();
            HostingUnitNameTextBox.Text = updateUnit.HostingUnitName;
            AreaTextBlock.Text = currentUnit.Area.ToString();
            SubAreaTextBlock.Text = currentUnit.SubArea.ToString();
            AdultSlider.Value = currentUnit.Adults;
            ChildrenSlider.Value = updateUnit.Children;
            AttractionCheckBox.IsChecked = updateUnit.ChildrensAttractions;
            GardenCheckBox.IsChecked = updateUnit.Garden;
            PoolCheckBox.IsChecked = updateUnit.Pool;
            JacuzziCheckBox.IsChecked = updateUnit.Jacuzzi;
            TypeComboBox.SelectedItem = currentUnit.Area;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            BL.IBL myBL = BlFactory.GetBL();
            updateUnit.HostingUnitName = HostingUnitNameTextBox.Text;
            updateUnit.Adults = int.Parse(AdultTextBox.Text);
            updateUnit.Children = int.Parse(ChildrenTextBox.Text);
            updateUnit.ChildrensAttractions = (bool)AttractionCheckBox.IsChecked;
            updateUnit.Garden = (bool)GardenCheckBox.IsChecked;
            updateUnit.Jacuzzi = (bool)JacuzziCheckBox.IsChecked;
            updateUnit.Pool = (bool)PoolCheckBox.IsChecked;
            updateUnit.Type = (type)TypeComboBox.SelectedItem;
            try
            {
                myBL.Update_Hosting_Unit(this.updateUnit);
            }
            catch(KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
        private void AdultTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AdultTextBox.Text != "")
                try
                {
                    AdultSlider.Value = int.Parse(AdultTextBox.Text);
                }
                catch (Exception ex)
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
    }
}
