using LibraryFor_CAR_DB.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Курсовая_работа._БД.Box;
using Курсовая_работа._БД.Classes;

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для VRCWindow.xaml
    /// </summary>
    public partial class VRCWindow : Window
    {
        BoxVRC _boxVRC = new BoxVRC();
        Broker broker1;
        public VRCWindow()
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
            _boxVRC.UpdateVRC();
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
            this.listView.DataContext = _boxVRC.GetVRC();
            this.listView.ItemsSource = _boxVRC.GetVRC();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Vrc vrc = new Vrc();
            vrc.CarPlate = this.groupBoxCarPlate.Text;
            vrc.NumberVrc = this.groupBoxNumberVRC.Text;
            vrc.IdPlace = int.Parse(this.groupBoxIDPlace.Text);
            vrc.IdVehicle = int.Parse(this.groupBoxIDVehicle.Text);
            vrc.IdDriver = int.Parse(this.groupBoxIDDriver.Text);
            vrc.DateIssued = DateOnly.Parse(this.groupBoxDateIssued.Text);
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != null)
            {
                _boxVRC.edit(vrc, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            Vrc vrc = new Vrc();
            vrc.CarPlate = this.groupBoxCarPlate.Text;
            vrc.NumberVrc = this.groupBoxNumberVRC.Text;
            vrc.IdPlace = int.Parse(this.groupBoxIDPlace.Text);
            vrc.IdVehicle = int.Parse(this.groupBoxIDVehicle.Text);
            vrc.IdDriver = int.Parse(this.groupBoxIDDriver.Text);
            vrc.DateIssued = DateOnly.Parse(this.groupBoxDateIssued.Text);
            _boxVRC.SaveVRC(vrc);
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
        private void clearForm()
        {
            this.groupBoxID.Content = "ID: ";
            this.groupBoxIDDriver.Text = "";
            this.groupBoxIDVehicle.Text = "";
            this.groupBoxDateIssued.Text = "";
            this.groupBoxCarPlate.Text = "";
            this.groupBoxNumberVRC.Text = "";
            this.groupBoxIDPlace.Text = "";
            this.groupBoxDateIssued.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            Vrc vrc = (Vrc)this.listView.SelectedItem;
            if (vrc != null)
            {
                this.groupBoxID.Content = "ID: " + vrc.Id;
                this.groupBoxIDDriver.Text = vrc.IdDriver.ToString();
                this.groupBoxIDVehicle.Text = vrc.IdVehicle.ToString();
                this.groupBoxDateIssued.Text = vrc.DateIssued.ToString();
                this.groupBoxCarPlate.Text = vrc.CarPlate;
                this.groupBoxNumberVRC.Text = vrc.NumberVrc;
                this.groupBoxIDPlace.Text = vrc.IdPlace.ToString();
                this.groupBoxDateIssued.Text = vrc.DateIssued.ToString();
                this.groupBtnClear.IsEnabled = true;
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            var item = button.DataContext as Vrc;
            _boxVRC.delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            Vrc vrc = new Vrc();
            vrc.CarPlate = this.groupBoxCarPlate.Text;
            vrc.NumberVrc = this.groupBoxNumberVRC.Text;
            if(this.groupBoxIDPlace.Text != "")
            {
                vrc.IdPlace = int.Parse(this.groupBoxIDPlace.Text);
            }
            if (this.groupBoxDateIssued.Text != "")
            {
                vrc.DateIssued = DateOnly.Parse(this.groupBoxDateIssued.Text);
            }
            if (this.groupBoxIDVehicle.Text != "")
            {
                vrc.IdVehicle = int.Parse(this.groupBoxIDVehicle.Text);
            }
            if (this.groupBoxIDDriver.Text != "")
            {
                vrc.IdDriver = int.Parse(this.groupBoxIDDriver.Text);
            }
            return _boxVRC.checkSameinDB(vrc);
        }

        private void choosingBtn_IDdriver_click(object sender, RoutedEventArgs e)
        {
            int id = broker1.StartWork("Driver", null, 1);
            if (id != -1)
            {
                this.groupBoxIDDriver.Text = broker1.ResultId.ToString();
            }
        }

        private void choosingBtn_IDVehicle_click(object sender, RoutedEventArgs e)
        {
            List<int> limitValues = new List<int>();
            List<Vrc> vrc = _boxVRC.GetVRC();
            if (vrc != null)
            {
                for (int i = 0; i < vrc.Count; i++)
                {
                    limitValues.Add((int)vrc[i].IdVehicle);
                }
            }
            int id = broker1.StartWork("Vehicle", limitValues, 1);
            if (id != -1)
            {
                this.groupBoxIDVehicle.Text = broker1.ResultId.ToString();
            }
        }
        private void choosingBtn_IDDepartments_click(object sender, RoutedEventArgs e)
        {
            int id = broker1.StartWork("PoliceDepartment", null, 1);
            if (id != -1)
            {
                this.groupBoxIDPlace.Text = broker1.ResultId.ToString();
            }
        }
        private void groupBoxsDataContextChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckAndEnable();
        }
        private void groupBoxsDataContextChanged(object sender, TextChangedEventArgs e)
        {
            CheckAndEnable();
        }
        private void CheckAndEnable()
        {
            if (this.groupBoxNumberVRC.Text != "" & this.groupBoxCarPlate.Text != "" & !checkSameinDB()
              & this.groupBoxIDDriver.Text != "" & this.groupBoxDateIssued.Text != "" & this.groupBoxIDVehicle.Text != "" &
              this.groupBoxDateIssued.Text != "" & this.groupBoxIDPlace.Text != "")
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
                this.groupBtnClear.IsEnabled = true;
            }
            else
            {
                if (this.groupBoxNumberVRC.Text != "" || this.groupBoxCarPlate.Text != ""
                || this.groupBoxIDDriver.Text != "" || this.groupBoxDateIssued.Text != "" || this.groupBoxIDVehicle.Text != "" ||
                this.groupBoxDateIssued.Text != "" || this.groupBoxIDPlace.Text != "")
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
            CheckAndFilter();
        }
        private void SearchBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckAndFilter();
        }
        private void CheckAndFilter()
        {
            if (this.searchBoxID.Text == "" & this.searchBoxIdDriver.Text == ""
              & this.searchBoxIdVehicle.Text == "" & this.searchBoxDateIssued.Text == ""
              & this.searchBoxNumberVRC.Text == "" & this.searchBoxCarPlate.Text == "" 
              & this.searchBoxIdPlace.Text =="")
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
            this.listView.ItemsSource = _boxVRC.filterVRC(this.searchBoxID.Text, this.searchBoxNumberVRC.Text,
                this.searchBoxCarPlate.Text, this.searchBoxIdDriver.Text, this.searchBoxDateIssued.Text,
                this.searchBoxIdVehicle.Text, this.searchBoxIdPlace.Text);
        }
    }
}
