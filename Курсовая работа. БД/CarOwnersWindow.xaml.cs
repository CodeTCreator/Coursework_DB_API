using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


namespace Курсовая_работа._БД
{
    /// <summary>
    /// Логика взаимодействия для CarOwnersWindow.xaml
    /// </summary>
    public partial class CarOwnersWindow : Window
    {
        Service_Driver service_Driver = new Service_Driver();
        List<LibraryFor_CAR_DB.Entity.Driver>? drivers = null;

        public CarOwnersWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }
        public void getDB()
        {
            drivers = service_Driver.getAllDriver();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            getDB();
            printDB();
        }
        public void printDB()
        {
            this.listView.DataContext = drivers;
            this.listView.ItemsSource = drivers;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
            this.groupBtnClear.IsEnabled = false;
            this.groupBtnAdd.IsEnabled = false;
            this.groupBtnSave.IsEnabled = false;
        }
        private void clearForm()
        {
            this.groupBoxID.Content = "ID: ";
            this.groupBoxFIO.Text = "";
            this.groupBoxAddress.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Driver driver = new Driver();
            driver.Fio = this.groupBoxFIO.Text;
            driver.Address = this.groupBoxAddress.Text;
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != "")
            {
                service_Driver.edit(driver, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            Driver driver = new Driver();
            driver.Fio = this.groupBoxFIO.Text;
            driver.Address = this.groupBoxAddress.Text;
            service_Driver.saveDriver(driver);
            getDB();
            printDB();
            clearForm();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as Driver;
            service_Driver.delete(item);
            getDB();
            printDB();
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            Driver driver = (Driver)this.listView.SelectedItem;
            if (driver != null)
            {
                this.groupBoxID.Content = "ID: " + driver.Id;
                this.groupBoxFIO.Text = driver.Fio;
                this.groupBoxAddress.Text = driver.Address;
                this.groupBtnClear.IsEnabled = true;
            }
        }
        private void groupBoxsDataContextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.groupBoxFIO.Text != "" & this.groupBoxAddress.Text != "")
            {
                if (checkSameinDB()) {
                    this.groupBtnAdd.IsEnabled = false;
                    this.groupBtnSave.IsEnabled = false;
                }
                else
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
                this.groupBtnClear.IsEnabled = true;
            }
            else
            {
                this.groupBtnAdd.IsEnabled = false;
                this.groupBtnSave.IsEnabled = false;
            }
        }

        private bool checkSameinDB()
        {
            string FIO = this.groupBoxFIO.Text;
            string Address = this.groupBoxAddress.Text;
            foreach (var item in drivers)
            {
                if (item.Fio.ToString() == FIO & item.Address.ToString()
                    == Address)
                {
                    return true;
                }
            }
            return false;
        }



        private void SearchBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (this.searchBoxFIO.Text == "" & this.searchBoxAddress.Text == "" & this.searchBoxID.Text == "")
            {
                printDB();
            }
            else
            {
                FilterList();
            }
        }
        private void FilterList()
        {
            var list = (from item in drivers
                        where
                        item.Id.ToString().Contains(this.searchBoxID.Text) &&
                        item.Fio.Contains(this.searchBoxFIO.Text) &&
                        item.Address.Contains(this.searchBoxAddress.Text)               
                        select item);
            this.listView.ItemsSource = list;
        }
    }
}
