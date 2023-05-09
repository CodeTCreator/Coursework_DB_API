using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Курсовая_работа._БД.Box;
using Курсовая_работа._БД.Classes;

namespace Курсовая_работа._БД
{
    /// <summary>
    /// Логика взаимодействия для DriverLicense.xaml
    /// </summary>
    public partial class DriverLicenseWindow : Window
    {
        BoxLicenses boxLicenses = new BoxLicenses(); 
        Broker broker1;
        public DriverLicenseWindow()
        {
            InitializeComponent();
            broker1 = new Broker();
        }   
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }
        public void getDB()
        {
            boxLicenses.UpdateLicenses();
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
            this.listView.DataContext = boxLicenses.GetDriversLicense();
            this.listView.ItemsSource = boxLicenses.GetDriversLicense();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DriversLicense driverLicense = new DriversLicense();
            driverLicense.IdDriver = int.Parse(this.groupBoxIDdriver.Text);
            driverLicense.IdPlaceOfIssue = int.Parse(this.groupBoxIDPlaceIssue.Text);
            driverLicense.ValidUntil = DateOnly.Parse(this.groupBoxValidUntil.Text);
            driverLicense.DateOfIssue = DateOnly.Parse(this.groupBoxDateIssue.Text);
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != null)
            {
                boxLicenses.Edit(driverLicense, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            DriversLicense driverLicense = new DriversLicense();
            driverLicense.IdDriver = int.Parse(this.groupBoxIDdriver.Text);
            driverLicense.IdPlaceOfIssue = int.Parse(this.groupBoxIDPlaceIssue.Text);
            driverLicense.ValidUntil = DateOnly.Parse(this.groupBoxValidUntil.Text);
            driverLicense.DateOfIssue = DateOnly.Parse(this.groupBoxDateIssue.Text);
            boxLicenses.SaveDriverLicense(driverLicense);
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
            this.groupBoxIDdriver.Text = "";
            this.groupBoxIDPlaceIssue.Text = "";
            this.groupBoxDateIssue.Text = "";
            this.groupBoxValidUntil.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            DriversLicense driversLicense = (DriversLicense)this.listView.SelectedItem;
            if (driversLicense != null)
            {
                this.groupBoxID.Content = "ID: " + driversLicense.Id;
                this.groupBoxIDdriver.Text = driversLicense.IdDriver.ToString();
                this.groupBoxIDPlaceIssue.Text = driversLicense.IdPlaceOfIssue.ToString();
                this.groupBoxDateIssue.Text = driversLicense.DateOfIssue.ToString();
                this.groupBoxValidUntil.Text = driversLicense.ValidUntil.ToString();
                this.groupBtnClear.IsEnabled = true;
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as DriversLicense;
            boxLicenses.Delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            return boxLicenses.CheckSameinDB(this.groupBoxIDdriver.Text, this.groupBoxIDPlaceIssue.Text,
                this.groupBoxDateIssue.Text, this.groupBoxValidUntil.Text);
        }
        private void groupBoxsDataContextChanged(object sender, TextChangedEventArgs e)
        {
            checkAndEnable();
        }
        private void SearchBoxChanged(object sender, TextChangedEventArgs e)
        {
            if (this.searchBoxID.Text == "" & this.searchBoxIDdriver.Text == "" & this.searchBoxPlaceIssue.Text == "" &
                this.searchBoxDateIssue.Text == "" & this.searchBoxValidUntil.Text == "")
            {
                printDB();
            }
            else
            {
                FilterList();
            }
        }
        private void SearchBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.searchBoxID.Text == "" & this.searchBoxIDdriver.Text == "" & this.searchBoxPlaceIssue.Text == "" &
                this.searchBoxDateIssue.Text == "" & this.searchBoxValidUntil.Text == "")
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
        }


        private void SearchBtn_click(object sender, RoutedEventArgs e)
        {
        }

        private void choosingBtn_IDdriver_click(object sender, RoutedEventArgs e)
        {
            List<int> limitValues = new List<int>();
            List<DriversLicense> driversLicenses= boxLicenses.GetDriversLicense();
            if(driversLicenses != null)
            {
                for (int i = 0; i < driversLicenses.Count; i++)
                {
                    limitValues.Add(driversLicenses[i].IdDriver);
                }
            }
            int id = broker1.StartWork("Driver", limitValues,1);
            if(id != -1)
            {
                this.groupBoxIDdriver.Text = broker1.ResultId.ToString();
            }
           
        }

        private void choosingBtn_IDPlaceIssue_click(object sender, RoutedEventArgs e)
        {
            int id = broker1.StartWork("PoliceDepartment", null,1);
            if(id != -1)
            {
                this.groupBoxIDPlaceIssue.Text = broker1.ResultId.ToString();
            } 
        }
        private void groupBoxDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            checkAndEnable();
        }

        private void checkAndEnable()
        {
            if (this.groupBoxIDdriver.Text != "" & this.groupBoxIDPlaceIssue.Text != "" & !checkSameinDB()
              & this.groupBoxDateIssue.Text != "" & this.groupBoxValidUntil.Text != "")
            {
                if (checkAndPaint())
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
                if (this.groupBoxIDdriver.Text != "" || this.groupBoxIDPlaceIssue.Text != ""
               || this.groupBoxDateIssue.Text != "" || this.groupBoxValidUntil.Text != "")
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
            checkAndPaint();
        }

        private bool checkAndPaint()
        {
            if (this.groupBoxDateIssue.Text != "" & this.groupBoxValidUntil.Text != "")
            {
                if (this.groupBoxDateIssue.SelectedDate >= this.groupBoxValidUntil.SelectedDate)
                {
                    this.groupBoxDateIssue.Background = new SolidColorBrush(Colors.Red);
                    this.groupBoxValidUntil.Background = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    this.groupBoxDateIssue.Background = new SolidColorBrush(Colors.White);
                    this.groupBoxValidUntil.Background = new SolidColorBrush(Colors.White);
                    return true;
                }
            }
            return false;
        }

        private void FilterList()
        {

            this.listView.ItemsSource = boxLicenses.FilterLicenses(this.searchBoxID.Text, this.searchBoxIDdriver.Text,
                this.searchBoxPlaceIssue.Text, this.searchBoxDateIssue.Text, this.searchBoxValidUntil.Text);
        }
    }
}
