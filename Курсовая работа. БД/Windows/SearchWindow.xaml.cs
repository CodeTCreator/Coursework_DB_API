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

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        BoxSearch _boxSearch = new BoxSearch();
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }
        public void getDB()
        {
            _boxSearch.UpdateSearches();
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
            this.listView.DataContext = _boxSearch.GetSearches();
            this.listView.ItemsSource = _boxSearch.GetSearches();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.CarPlate = this.groupBoxCarPlate.Text;
            search.FullName = this.groupBoxFullName.Text;
            search.Level = this.groupBoxLevel.SelectedIndex + 1;
            search.Signs = this.groupBoxSigns.Text;
            search.Color = this.groupBoxColor.Text;
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != null)
            {
                _boxSearch.edit(search, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            Search search = new Search();
            search.CarPlate = this.groupBoxCarPlate.Text;
            search.FullName = this.groupBoxFullName.Text;
            search.Level = this.groupBoxLevel.SelectedIndex + 1;
            search.Signs = this.groupBoxSigns.Text;
            search.Color = this.groupBoxColor.Text;
            _boxSearch.SaveSearch(search);
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
            this.groupBoxCarPlate.Text = "";
            this.groupBoxFullName.Text = "";
            this.groupBoxLevel.Text = "";
            this.groupBoxSigns.Text = "";
            this.groupBoxColor.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            Search search = (Search)this.listView.SelectedItem;
            if (search != null)
            {
                this.groupBoxID.Content = "ID: " + search.Id;
                this.groupBoxCarPlate.Text = search.CarPlate;
                this.groupBoxFullName.Text = search.FullName;
                this.groupBoxLevel.SelectedIndex = (int)search.Level - 1;
                this.groupBoxSigns.Text = search.Signs;
                this.groupBoxColor.Text = search.Color;
                this.groupBtnClear.IsEnabled = true;
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as Search;
            _boxSearch.delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            Search search = new Search();
            search.CarPlate = this.groupBoxCarPlate.Text;
            search.FullName = this.groupBoxFullName.Text;
            search.Level = this.groupBoxLevel.SelectedIndex + 1;
            search.Signs = this.groupBoxSigns.Text;
            search.Color = this.groupBoxColor.Text;
            return _boxSearch.checkSameinDB(search);
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
            if (this.groupBoxCarPlate.Text != "" & this.groupBoxFullName.Text != "" & !checkSameinDB()
               & this.groupBoxSigns.Text != "" & this.groupBoxColor.Text != "" &
              this.groupBoxLevel.SelectedIndex.ToString() != "-1")
            {
                if (checkSameinDB())
                {
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
                if (this.groupBoxCarPlate.Text != "" || this.groupBoxFullName.Text != ""
                 || this.groupBoxLevel.SelectedIndex.ToString() != "-1" || this.groupBoxSigns.Text != "" ||
                this.groupBoxColor.Text != "")
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

            if (this.searchBoxID.Text == "" & this.searchBoxCarPlate.Text == "" & this.searchBoxFullName.Text == "" &
                this.searchBoxColor.Text == "" & this.searchBoxSigns.Text == "" & this.searchBoxLevel.Text == "")
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
            this.listView.ItemsSource = _boxSearch.filterSearches(this.searchBoxID.Text,
                this.searchBoxCarPlate.Text, this.searchBoxLevel.Text, this.searchBoxFullName.Text,
                this.searchBoxColor.Text, this.searchBoxSigns.Text);
        }
    }
}
