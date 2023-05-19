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
        Broker broker1;
        public MinuteWindow()
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
            _boxMinutes.UpdateMinutes();
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
            //List<int> limitValues = new List<int>();
            //List<Vrc> vrc = _boxMinutes.GetVRC();
            //if (vrc != null)
            //{
            //    for (int i = 0; i < vrc.Count; i++)
            //    {
            //        limitValues.Add((int)vrc[i].IdDriver);
            //    }
            //}
            int id = broker1.StartWork("DriverLicense", null, 1);
            if (id != -1)
            {
                this.groupBoxIdViolator.Text = broker1.ResultId.ToString();
            }
        }

        private void choosingBtn_IDInspector_click(object sender, RoutedEventArgs e)
        {
            //List<int> limitValues = new List<int>();
            //List<Vrc> vrc = _boxMinutes.GetVRC();
            //if (vrc != null)
            //{
            //    for (int i = 0; i < vrc.Count; i++)
            //    {
            //        limitValues.Add((int)vrc[i].IdVehicle);
            //    }
            //}
            int id = broker1.StartWork("Inspector", null, 1);
            if (id != -1)
            {
                this.groupBoxIdInspector.Text = broker1.ResultId.ToString();
            }
        }
        private void choosingBtn_IDVrc_click(object sender, RoutedEventArgs e)
        {
            int id = broker1.StartWork("Vrc", null, 1);
            if (id != -1)
            {
                this.groupBoxIdVrc.Text = broker1.ResultId.ToString();
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
            if (this.groupBoxIdViolator.Text != "" & this.groupBoxIdInspector.Text != "" & !checkSameinDB()
              & this.groupBoxIdVrc.Text != "" & this.groupBoxExplanation.Text != "" & this.groupBoxCrimeScene.Text != "" &
              this.groupBoxDateIssued.Text != "" & this.groupBoxArticles.Text != "")
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
                if (this.groupBoxIdViolator.Text != "" || this.groupBoxIdInspector.Text != "" || !checkSameinDB()
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

        }
    }
}
