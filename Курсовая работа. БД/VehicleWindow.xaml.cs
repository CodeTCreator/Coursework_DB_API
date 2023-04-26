
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;


namespace Курсовая_работа._БД
{
    /// <summary>
    /// Логика взаимодействия для Vehicle.xaml
    /// </summary>
    public partial class VehicleWindow : Window
    {

        Service_Vehicle service_Vehicle = new Service_Vehicle();
        List<LibraryFor_CAR_DB.Entity.Vehicle>? vehicles = null;
        public VehicleWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }
        public void getDB()
        {
            vehicles = service_Vehicle.getAllVehicle();
        }

        //Обработка кнопок
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        public void printDB()
        {
            this.listView.DataContext = vehicles;
            this.listView.ItemsSource = vehicles;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Body = this.groupBoxBody.Text;
            vehicle.BrandModel = this.groupBoxBrand.Text;
            vehicle.Color = this.groupBoxColor.Text;
            vehicle.EngineCapacity = float.Parse(this.groupBoxEngine.Text, CultureInfo.InvariantCulture.NumberFormat);
            vehicle.CarPower = float.Parse(this.groupBoxPower.Text, CultureInfo.InvariantCulture.NumberFormat);
            vehicle.Vin = this.groupBoxVin.Text;
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != null)
            {
                service_Vehicle.edit(vehicle, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            Vehicle vehicle = new Vehicle();
            vehicle.Body = groupBoxBody.Text;
            vehicle.BrandModel = groupBoxBrand.Text;
            vehicle.CarPower = float.Parse(groupBoxPower.Text, CultureInfo.InvariantCulture.NumberFormat);
            vehicle.Color = groupBoxColor.Text;
            vehicle.EngineCapacity = float.Parse(groupBoxEngine.Text, CultureInfo.InvariantCulture.NumberFormat);
            vehicle.Vin = groupBoxVin.Text;
            service_Vehicle.saveVehicle(vehicle);
            getDB();
            printDB();
            clearForm();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
            this.groupBtnClear.IsEnabled = false;
            this.groupBtnAdd.IsEnabled = false;
            this.groupBtnSave.IsEnabled = false;
        }

        private void disenabledChangeButton()
        {
            this.groupBtnClear.IsEnabled = true;
            this.groupBtnAdd.IsEnabled = false;
            this.groupBtnSave.IsEnabled = false;
        }
        private void clearForm()
        {
            this.groupBoxID.Content = "ID: ";
            this.groupBoxBody.Text = "";
            this.groupBoxBrand.Text = "";
            this.groupBoxColor.Text = "";
            this.groupBoxEngine.Text = "";
            this.groupBoxPower.Text = "";
            this.groupBoxVin.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            Vehicle vehicle = (Vehicle)this.listView.SelectedItem;
            if (vehicle != null)
            {
                this.groupBoxID.Content = "ID: " + vehicle.Id;
                this.groupBoxBody.Text = vehicle.Body;
                this.groupBoxBrand.Text = vehicle.BrandModel;
                this.groupBoxColor.Text = vehicle.Color;
                this.groupBoxEngine.Text = vehicle.EngineCapacity.ToString().Replace(',', '.');
                this.groupBoxPower.Text = vehicle.CarPower.ToString();
                this.groupBoxVin.Text = vehicle.Vin;
                this.groupBtnClear.IsEnabled = true;
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as Vehicle;
            service_Vehicle.delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            string Body = this.groupBoxBody.Text;
            string Brand = this.groupBoxBrand.Text;
            string Color = this.groupBoxColor.Text;
            string engineCapacity = this.groupBoxEngine.Text;
            string carPower = this.groupBoxPower.Text;
            string Vin = this.groupBoxVin.Text;
            foreach (var item in vehicles)
            {
                if (item.CarPower.ToString() == carPower & item.EngineCapacity.ToString() 
                    == engineCapacity & item.Vin == Vin & item.Body == Body & item.BrandModel == Brand &
                    item.Color == Color)
                {
                    return true;
                }
            }
            return false;
        }
        private void groupBoxsDataContextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.groupBoxBody.Text != "" & this.groupBoxBrand.Text != "" & !checkSameinDB()
                & this.groupBoxColor.Text != "" & this.groupBoxEngine.Text != "" & this.groupBoxPower.Text != "" &
                this.groupBoxVin.Text != "")
            {
                if(Regex.IsMatch(this.groupBoxEngine.Text, @"^\d*\,?\d+$|^\d*\.?\d+$", RegexOptions.IgnoreCase)&
                    Regex.IsMatch(this.groupBoxPower.Text, @"^\d*\,?\d+$|^\d*\.?\d+$", RegexOptions.IgnoreCase))
                {
                    string? id = this.groupBoxID.Content.ToString();
                    id = id?.Substring(4);
                    if (id != "")
                    {
                        this.groupBtnSave.IsEnabled = true;
                    }
                    else
                    {
                        this.groupBtnAdd.IsEnabled = true;
                    }
                }
                else
                {
                    this.groupBtnAdd.IsEnabled = false;
                    this.groupBtnSave.IsEnabled = false;
                }
                this.groupBtnClear.IsEnabled = true;
            }
            else
            {
                this.groupBtnAdd.IsEnabled = false;
                this.groupBtnSave.IsEnabled = false;
            }

        }
        private void SearchBoxChanged(object sender, TextChangedEventArgs e)
        {

            if (this.searchBoxVIN.Text == "" & this.searchBoxBrand.Text == "" & this.searchBoxID.Text == "" &
                this.searchBoxBody.Text == "" & this.searchBoxColor.Text =="" & this.searchBoxEngineCapacity.Text == ""
                & this.searchBoxPower.Text == "")
            {
                printDB();
            }
            else
            {
                FilterList();
            }
        }

        private void NSearchBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (this.searchBoxID.Text != ""
                || this.searchBoxPower.Text != "" || this.searchBoxEngineCapacity.Text != "")
            {
                this.SearchBtn.IsEnabled = true;
            }
            else
            {
                this.SearchBtn.IsEnabled = false;
            }
        }


      
        private void SearchBtn_click(object sender, RoutedEventArgs e)
        {
            List<LibraryFor_CAR_DB.Entity.Vehicle> list, list1, list2, list3;
            list = vehicles; list1 = vehicles; list2 = vehicles; list3 = vehicles;

            if (this.searchBoxID.Text != "")
            {
                list1 = ((List<Vehicle>)(from item in list
                        where
                         item.Id == int.Parse(this.searchBoxID.Text)
                        select item));
            }
            if (this.searchBoxPower.Text != "")
            {
                list2 = ((List<Vehicle>)(from item in list1
                                         where
                                         item.CarPower == (float.Parse(this.searchBoxPower.Text, CultureInfo.InvariantCulture.NumberFormat)) 
                                         select item));
            }
            if (this.searchBoxEngineCapacity.Text != "")
            {
                list3 = ((List<Vehicle>)(from item in list2
                                         where
                                         item.EngineCapacity.ToString() == this.searchBoxEngineCapacity.Text
                                         select item));
            }
            //FilterList(list3);
        }
        private void FilterList()
        {
            var list = (from item in vehicles
                        where
                        item.Id.ToString().Contains(this.searchBoxID.Text) &&
                        item.Vin.Contains(this.searchBoxVIN.Text) &&
                        item.Body.Contains(this.searchBoxBody.Text) &&
                        item.CarPower.ToString().Contains(this.searchBoxPower.Text) &&
                        item.EngineCapacity.ToString().Contains(this.searchBoxEngineCapacity.Text.Replace('.',',')) &&
                        item.BrandModel.Contains(this.searchBoxBrand.Text) &&
                        item.Color.Contains(this.searchBoxColor.Text)
                        select item);
            this.listView.ItemsSource = list;
        }
    }
}
