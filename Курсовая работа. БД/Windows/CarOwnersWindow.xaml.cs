using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Курсовая_работа._БД.Box;

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для CarOwnersWindow.xaml
    /// </summary>
    public partial class CarOwnersWindow : Window
    {
        BoxDrivers boxDrivers = new BoxDrivers();


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
            boxDrivers.updateDrivers();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            getDB();
            printDB();
        }
        public void printDB()
        {
            this.listView.DataContext = boxDrivers.GetDrivers();
            this.listView.ItemsSource = boxDrivers.GetDrivers();
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
                boxDrivers.edit(driver, int.Parse(id));
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
            boxDrivers.saveDriver(driver);
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
            boxDrivers.delete(item);
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
                if (this.groupBoxFIO.Text != "" || this.groupBoxAddress.Text != "")
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

        private bool checkSameinDB()
        {
           string FIO = this.groupBoxFIO.Text;
           string Address = this.groupBoxAddress.Text;
           return boxDrivers.checkSameinDB(FIO, Address);
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
            string ID = this.searchBoxID.Text;
            string FIO = this.searchBoxFIO.Text;
            string Address = this.searchBoxAddress.Text;
            this.listView.ItemsSource = boxDrivers.filterDrivers(ID,FIO,Address);
        }
    }
}
