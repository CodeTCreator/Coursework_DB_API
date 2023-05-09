
using LibraryFor_CAR_DB.Entity;
using LibraryFor_CAR_DB.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Курсовая_работа._БД.Box;
using Курсовая_работа._БД.ViewDialogWindow;

namespace Курсовая_работа._БД
{
    /// <summary>
    /// Логика взаимодействия для ChoosingDBWindow.xaml
    /// </summary>
    public partial class ChoosingDBWindow : Window
    {
        string nameDB = "";
        int selectedID = -1;
        List<int>? limitValues = null;
        bool buildWindow = false;
        int _mode = -1;


        BoxViehiclex _boxViehicle;
        BoxDrivers _boxDrivers;
        BoxLicenses _boxLicenses;
        BoxDepartments _boxDepartment;

        IViewDialogWindow viewDW;

        public int SelectedID
        {
            get {return selectedID; }
        }

        public int Mode
        {
            get { return _mode; }
            set 
            { 
                _mode = value; 
                CheckMode(); 
            }
        }
        public string NameDB
        {
            get { return nameDB; }
            set 
            { 
                nameDB = value;
                setView();
                CheckMode();
            }
        }

        private void CheckMode()
        {
            if(Mode == -1)
            {
                this.selectionBtn.Visibility = Visibility.Hidden;
            }
        }
        private void setView()
        {
            if(nameDB == "Driver")
            {
                _boxDrivers = new BoxDrivers();
                viewDW = new ViewDriver();
            }
            if(nameDB == "PoliceDepartment")
            {
                _boxDepartment = new BoxDepartments();
                viewDW = new ViewDepartment();
            }
        }
        public List<int> LimitValues
        {
            get { return limitValues; }
            set { limitValues = value; }
        }

        public ChoosingDBWindow()
        {
            InitializeComponent();
            buildWindow = true;
        }

        private void createView()
        {
           
            if (NameDB == "Driver")
            {
                viewDW = new ViewDriver();
                createViewDriver();
            }
            if (NameDB == "PoliceDepartment")
            {
                
                viewDW = new ViewDepartment();
                createViewPoliceDepartment();
            }
            this.listViewData.View = viewDW.getGridView();
            this.Width = viewDW.getWidth() + 100;
            Thickness thicknessBack = new Thickness(this.Width - this.backBtn.Width - 30, this.Height - this.backBtn.Height - 50, 0, 0);
            this.backBtn.Margin = thicknessBack;
            Thickness thicknessChoose = new Thickness(this.Width - this.backBtn.Width - 50 - this.selectionBtn.Width, this.Height - this.selectionBtn.Height - 50, 0, 0);
            this.selectionBtn.Margin = thicknessChoose;
            Style style = new Style(typeof(ListViewItem));
            style.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            this.listViewData.ItemContainerStyle = style;
        }


        private void createViewDriver()
        {
            this.labelSeacrh.Content = "Поиск (ФИО):";
            this.listViewData.DataContext = _boxDrivers.filterLimitsID(limitValues);
            this.listViewData.ItemsSource = _boxDrivers.filterLimitsID(limitValues);
        }

        private void createViewPoliceDepartment()
        {
            this.labelSeacrh.Content = "Поиск (местоположение):";
            this.listViewData.DataContext = _boxDepartment.GetPoliceDepartments();
            this.listViewData.ItemsSource = _boxDepartment.GetPoliceDepartments();
        }
        private void listViewData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(nameDB == "Driver")
            {
                Driver driver = (Driver)this.listViewData.SelectedItem;
                if(driver!= null)
                {
                    this.labelChoose.Content = "Id: " + driver.Id + " | ФИО: " + driver.Fio + " | Адрес: " + driver.Address;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = driver.Id;
                }
            }
            if(nameDB == "PoliceDepartment")
            {
                PoliceDepartment policeDepartment = (PoliceDepartment)this.listViewData.SelectedItem;
                if (policeDepartment != null)
                {
                    this.labelChoose.Content = "Id: " + policeDepartment.Id + " | Местоположение: " + policeDepartment.Location;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = policeDepartment.Id;
                }
                
            }
            
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedID = -1;
            this.Close();
        }
        private void selectionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void buildSkeleton()
        {
            if (buildWindow)
            {
                createView();
            }
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
