
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

namespace Курсовая_работа._БД.Windows
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


        BoxViehicles _boxViehicle;
        BoxDrivers _boxDrivers;
        BoxLicenses _boxLicenses;
        BoxDepartments _boxDepartment;
        BoxInspectors _boxInspectors;
        BoxVRC _boxVrc;

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
            }
            if(nameDB == "PoliceDepartment")
            {
                _boxDepartment = new BoxDepartments();
            }
            if(nameDB == "Vehicle")
            {
                _boxViehicle= new BoxViehicles();
            }
            if (nameDB == "Inspector")
            {
                _boxInspectors = new BoxInspectors();
            }
            if (nameDB == "DriverLicense")
            {
                _boxLicenses = new BoxLicenses();
            }
            if (nameDB == "Vrc")
            {
                _boxVrc = new BoxVRC();
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
            if (NameDB == "DriverLicense")
            {
                viewDW = new ViewLicenses();
                createViewLicense();
            }
            if (NameDB == "Vehicle")
            {
                viewDW = new ViewVehicle();
                createViewVehicle();
            }
            if (NameDB == "Inspector")
            {
                viewDW = new ViewInspector();
                createViewInspector();
            }
            if (NameDB == "Vrc")
            {
                viewDW = new ViewVRC();
                createViewVrc();
            }
            this.listViewData.View = viewDW.GetGridView();
            this.Width = viewDW.GetWidth() + 100;
            this.labelChoose.Width= this.Width - 80;
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
        private void createViewVehicle()
        {
            this.labelSeacrh.Content = "Поиск (VIN):";
            this.listViewData.DataContext = _boxViehicle.FilterLimitsID(limitValues);
            this.listViewData.ItemsSource = _boxViehicle.FilterLimitsID(limitValues);
        }
        private void createViewLicense()
        {
            this.labelSeacrh.Content = "Поиск (VIN):";
            this.listViewData.DataContext = _boxLicenses.FilterLimitsID(limitValues);
            this.listViewData.ItemsSource = _boxLicenses.FilterLimitsID(limitValues);
        }

        private void createViewPoliceDepartment()
        {
            this.labelSeacrh.Content = "Поиск (местоположение):";
            this.listViewData.DataContext = _boxDepartment.GetPoliceDepartments();
            this.listViewData.ItemsSource = _boxDepartment.GetPoliceDepartments();
        }
        private void createViewInspector()
        {
            this.labelSeacrh.Content = "Поиск (ФИО):";
            this.listViewData.DataContext = _boxInspectors.GetInspectors();
            this.listViewData.ItemsSource = _boxInspectors.GetInspectors();
        }
        private void createViewVrc()
        {
            this.labelSeacrh.Content = "Поиск (Номер свидетельства):";
            this.listViewData.DataContext = _boxVrc.GetVRC();
            this.listViewData.ItemsSource = _boxVrc.GetVRC();
        }
        private void listViewData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(nameDB == "Driver")
            {
                Driver driver = (Driver)this.listViewData.SelectedItem;
                if(driver!= null)
                {
                    this.labelChoose.Text = "Id: " + driver.Id + " | ФИО: " + driver.Fio + " | Адрес: " + driver.Address;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = driver.Id;
                }
            }
            if(nameDB == "PoliceDepartment")
            {
                PoliceDepartment policeDepartment = (PoliceDepartment)this.listViewData.SelectedItem;
                if (policeDepartment != null)
                {
                    this.labelChoose.Text = "Id: " + policeDepartment.Id + " | Местоположение: " + policeDepartment.Location;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = policeDepartment.Id;
                }
            }
            if (nameDB == "Vehicle")
            {
                Vehicle vehicle = (Vehicle)this.listViewData.SelectedItem;
                if (vehicle != null)
                {
                    this.labelChoose.Text = "Id: " + vehicle.Id + " | VIN: " + vehicle.Vin + " | Модель: " + vehicle.BrandModel
                        + " | Объем двигателя: " + vehicle.EngineCapacity + "\n" + "| Мощность: " + vehicle.CarPower
                        + " | Кузов: " + vehicle.Body + " | Цвет: " + vehicle.Color;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = vehicle.Id;
                }
            }
            if (nameDB == "DriverLicense")
            {
                DriversLicense driverLicense = (DriversLicense)this.listViewData.SelectedItem;
                if (driverLicense != null)
                {
                    this.labelChoose.Text = "Id: " + driverLicense.Id + " | id водителя: " + driverLicense.IdDriver + " | id места выдачи: " + driverLicense.IdPlaceOfIssue
                        + " | Дата выдачи: " + driverLicense.DateOfIssue + "\n" + "| Действительно до: " + driverLicense.ValidUntil;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = driverLicense.Id;
                }
            }
            if (nameDB == "Inspector")
            {
                Inspector inspector = (Inspector)this.listViewData.SelectedItem;
                if (inspector != null)
                {
                    this.labelChoose.Text = "Id: " + inspector.Id + " | ФИО: " + inspector.FullName + " | Звание: " + inspector.Rank
                        + " | Дата рождения: " + inspector.Birthday + "\n";
                    this.selectionBtn.IsEnabled = true;
                    selectedID = inspector.Id;
                }
            }
            if (nameDB == "Vrc")
            {
                Vrc vrc = (Vrc)this.listViewData.SelectedItem;
                if (vrc != null)
                {
                    this.labelChoose.Text = "Id: " + vrc.Id + " | Номер свидетельства: " + vrc.NumberVrc+ " | Номер авто: " + vrc.CarPlate + "\n"
                        + " | id места выдачи: " + vrc.IdPlace + " | Дата выдачи: " + vrc.DateIssued + " |id владельца: " + vrc.IdDriver;
                    this.selectionBtn.IsEnabled = true;
                    selectedID = vrc.Id;
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


            if(_boxDrivers != null)
            {
                this.listViewData.DataContext = _boxDrivers.FilterFIODrivers(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxDrivers.FilterFIODrivers(this.searchBox.Text);
            }
            if (_boxViehicle != null)
            {
                this.listViewData.DataContext = _boxViehicle.filterVINVehicles(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxViehicle.filterVINVehicles(this.searchBox.Text);
            }
            if (_boxLicenses != null)
            {
                this.listViewData.DataContext = _boxLicenses.filterIDdriverLicenses(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxLicenses.filterIDdriverLicenses(this.searchBox.Text);
            }
            if (_boxDepartment != null)
            {
                this.listViewData.DataContext = _boxDepartment.filterLocations(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxDepartment.filterLocations(this.searchBox.Text);
            }
            if (_boxInspectors != null)
            {
                this.listViewData.DataContext = _boxInspectors.FilterFIOInspectors(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxInspectors.FilterFIOInspectors(this.searchBox.Text);
            }
            if (_boxVrc != null)
            {
                this.listViewData.DataContext = _boxVrc.FilterNumber(this.searchBox.Text);
                this.listViewData.ItemsSource = _boxVrc.FilterNumber(this.searchBox.Text);
            }
        }
    }
}
