
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using Курсовая_работа._БД.Box;  

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для Vehicle.xaml
    /// </summary>
    public partial class VehicleWindow : Window
    {

        BoxViehicles boxViehicle = new BoxViehicles();
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
            boxViehicle.UpdateVehicles();
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
            this.listView.DataContext = boxViehicle.GetVehicles();
            this.listView.ItemsSource = boxViehicle.GetVehicles();
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
                boxViehicle.edit(vehicle, int.Parse(id));
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
            boxViehicle.SaveVehicle(vehicle);
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
            boxViehicle.delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            Vehicle vehicle = new Vehicle();
            vehicle.Body = this.groupBoxBody.Text;
            vehicle.BrandModel = this.groupBoxBrand.Text;
            vehicle.Color = this.groupBoxColor.Text;
            if (this.groupBoxEngine.Text != "" & Regex.IsMatch(this.groupBoxEngine.Text, @"^\d*\,?\d+$|^\d*\.?\d+$", RegexOptions.IgnoreCase))
            {
                vehicle.EngineCapacity = float.Parse(this.groupBoxEngine.Text, CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                vehicle.EngineCapacity = 0;
            }
            if (this.groupBoxPower.Text != "" & Regex.IsMatch(this.groupBoxPower.Text, @"^\d*\,?\d+$|^\d*\.?\d+$", RegexOptions.IgnoreCase))
            {
                vehicle.CarPower = float.Parse(this.groupBoxPower.Text, CultureInfo.InvariantCulture.NumberFormat);
            }
            else
            {
                vehicle.CarPower = 0;
            }
            vehicle.Vin = this.groupBoxVin.Text;
            return boxViehicle.checkSameinDB(vehicle);
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
                if (this.groupBoxBody.Text != ""  || this.groupBoxBrand.Text != ""
                || this.groupBoxColor.Text != "" || this.groupBoxEngine.Text != "" || this.groupBoxPower.Text != "" ||
                this.groupBoxVin.Text != "")
                {
                    this.groupBtnClear.IsEnabled = true;
                }
                else
                {
                    this.groupBtnClear.IsEnabled = false;
                }
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
            //if (this.searchBoxID.Text != ""
            //    || this.searchBoxPower.Text != "" || this.searchBoxEngineCapacity.Text != "")
            //{
            //    this.SearchBtn.IsEnabled = true;
            //}
            //else
            //{
            //    this.SearchBtn.IsEnabled = false;
            //}
        }


      
        private void SearchBtn_click(object sender, RoutedEventArgs e)
        {
        }
        private void FilterList()
        {
            this.listView.ItemsSource = boxViehicle.filterVehicles(this.searchBoxID.Text,
                this.searchBoxVIN.Text, this.searchBoxBody.Text, this.searchBoxPower.Text,
                this.searchBoxEngineCapacity.Text.Replace('.', ','), this.searchBoxBrand.Text, this.searchBoxColor.Text);
        }
    }
}
