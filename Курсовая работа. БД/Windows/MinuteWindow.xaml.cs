using LibraryFor_CAR_DB.Entity;
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
using Курсовая_работа._БД.Box;
using Курсовая_работа._БД.Classes;

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для MinuteWindow.xaml
    /// </summary>
    public partial class MinuteWindow : Window
    {
        BoxMinutes _boxMinutes = new BoxMinutes();
        BoxArticles _boxArticles = new BoxArticles();
        Broker _broker1;
        List<String> _articles = new List<string>();
        public MinuteWindow()
        {
            InitializeComponent();
            _broker1 = new Broker();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }
        public void getDB()
        {
            _boxMinutes.UpdateMinutes();
            _boxArticles.UpdateArticles();
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
            this.listView.DataContext = _boxMinutes.GetMinutes();
            this.listView.ItemsSource = _boxMinutes.GetMinutes();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Minute minute = new Minute();
            minute.IdViolator = int.Parse(this.groupBoxIdViolator.Text);
            minute.IdInspector = int.Parse(this.groupBoxIdInspector.Text);
            minute.IdVrc = int.Parse(this.groupBoxIdVrc.Text);
            minute.CrimeScene = this.groupBoxCrimeScene.Text;
            minute.DateMinutes = DateOnly.Parse(this.groupBoxDateIssued.Text);
            minute.ExplanationViolator = this.groupBoxExplanation.Text;
            // Нарушенные статьи, запись их в другую таблицу

            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if (id != null)
            {
                _boxMinutes.Edit(minute, int.Parse(id));
            }
            getDB();
            printDB();
            clearForm();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            Minute minute = new Minute();
            minute.IdViolator = int.Parse(this.groupBoxIdViolator.Text);
            minute.IdInspector = int.Parse(this.groupBoxIdInspector.Text);
            minute.IdVrc = int.Parse(this.groupBoxIdVrc.Text);
            minute.CrimeScene = this.groupBoxCrimeScene.Text;
            minute.DateMinutes = DateOnly.Parse(this.groupBoxDateIssued.Text);
            minute.ExplanationViolator = this.groupBoxExplanation.Text;
            _boxMinutes.SaveMinute(minute);
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
            this.groupBoxIdViolator.Text = "";
            this.groupBoxIdInspector.Text = "";
            this.groupBoxDateIssued.Text = "";
            this.groupBoxIdVrc.Text = "";
            this.groupBoxCrimeScene.Text = "";
            this.groupBoxExplanation.Text = "";
            _articles.Clear();
            this.groupBoxArticles.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearForm();
            Minute minute = (Minute)this.listView.SelectedItem;
            if (minute != null)
            {
                this.groupBoxID.Content = "ID: " + minute.Id;
                this.groupBoxIdViolator.Text = minute.IdViolator.ToString();
                this.groupBoxIdInspector.Text = minute.IdInspector.ToString();
                this.groupBoxIdVrc.Text = minute.IdVrc.ToString();
                this.groupBoxCrimeScene.Text = minute.CrimeScene;
                this.groupBoxExplanation.Text = minute.ExplanationViolator;
               // this.groupBoxArticles.Text = vrc.IdPlace.ToString();
                this.groupBoxDateIssued.Text = minute.DateMinutes.ToString();
                this.groupBtnClear.IsEnabled = true;
                _articles = _boxArticles.GetArticles(minute.Id);
                this.groupBoxArticles.Text = String.Join("; ", _articles.ToArray());
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as Minute;
            _boxMinutes.Delete(item);
            getDB();
            printDB();
        }

        private bool checkSameinDB()
        {
            Minute minute = new Minute();
            minute.CrimeScene = this.groupBoxCrimeScene.Text;
            minute.ExplanationViolator = this.groupBoxExplanation.Text;
            if (this.groupBoxIdViolator.Text != "")
            {
                minute.IdViolator = int.Parse(this.groupBoxIdViolator.Text);
            }
            if (this.groupBoxDateIssued.Text != "")
            {
                minute.DateMinutes = DateOnly.Parse(this.groupBoxDateIssued.Text);
            }
            if (this.groupBoxIdInspector.Text != "")
            {
                minute.IdInspector = int.Parse(this.groupBoxIdInspector.Text);
            }
            if (this.groupBoxIdVrc.Text != "")
            {
                minute.IdVrc = int.Parse(this.groupBoxIdVrc.Text);
            }
            return _boxMinutes.CheckSameinDB(minute);
        }

        private void choosingBtn_IDViolator_click(object sender, RoutedEventArgs e)
        {
            int id = _broker1.StartWork("DriverLicense", null, 1);
            if (id != -1)
            {
                this.groupBoxIdViolator.Text = _broker1.ResultId.ToString();
            }
        }

        private void choosingBtn_IDInspector_click(object sender, RoutedEventArgs e)
        {
            int id = _broker1.StartWork("Inspector", null, 1);
            if (id != -1)
            {
                this.groupBoxIdInspector.Text = _broker1.ResultId.ToString();
            }
        }
        private void choosingBtn_IDVrc_click(object sender, RoutedEventArgs e)
        {
            int id = _broker1.StartWork("Vrc", null, 1);
            if (id != -1)
            {
                this.groupBoxIdVrc.Text = _broker1.ResultId.ToString();
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
            if (this.groupBoxIdViolator.Text != "" & this.groupBoxIdInspector.Text != ""
              & this.groupBoxIdVrc.Text != "" & this.groupBoxExplanation.Text != "" & this.groupBoxCrimeScene.Text != "" &
              this.groupBoxDateIssued.Text != "" || this.groupBoxArticles.Text != "")
            {
                string? id = this.groupBoxID.Content.ToString();
                this.groupBtnSave.IsEnabled = true;
                this.groupBtnAdd.IsEnabled = true;
                this.groupBtnClear.IsEnabled = true;
            }
            else
            {
                if (this.groupBoxIdViolator.Text != "" || this.groupBoxIdInspector.Text != "" 
                 || this.groupBoxIdVrc.Text != "" || this.groupBoxExplanation.Text != "" || this.groupBoxCrimeScene.Text != "" ||
                this.groupBoxDateIssued.Text != "" || this.groupBoxArticles.Text != "")
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
            if (this.searchBoxID.Text == "" & this.searchBoxIdViolator.Text == ""
              & this.searchBoxIdInspector.Text == "" & this.searchBoxIdVrc.Text == ""
              & this.searchBoxCrimeScene.Text == "" & this.searchBoxDateIssued.Text == "")
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
            this.listView.ItemsSource = _boxMinutes.FilterMinutes(this.searchBoxID.Text, this.searchBoxIdViolator.Text,
                this.searchBoxIdInspector.Text, this.searchBoxIdVrc.Text, this.searchBoxCrimeScene.Text,
                this.searchBoxDateIssued.Text);
        }

        private void choosingBtn_IDArticles_click(object sender, RoutedEventArgs e)
        {
           
            List<int> limitValues = new List<int>();
            List<String> limitValuesString = new List<String>();
            int id = -1;
            if (_articles.Count == 0)
            {
                
                id = _broker1.StartWork("CoAo", null, 1);
            }
            else
            {
                limitValuesString = _articles;
                limitValues = limitValuesString.Select(int.Parse).ToList();
                id = _broker1.StartWork("CoAo", limitValues, 1);
            }
            if (id != -1)
            {
                _articles.Add(_broker1.ResultId.ToString());
                this.groupBoxArticles.Text = String.Join("; ", _articles.ToArray()); 
            }
        }

        private void choosingBtn_DeleteArticles_click(object sender, RoutedEventArgs e)
        {
            _articles.RemoveAt(_articles.Count - 1);
            this.groupBoxArticles.Text = String.Join("; ", _articles.ToArray());
        }
    }
}
