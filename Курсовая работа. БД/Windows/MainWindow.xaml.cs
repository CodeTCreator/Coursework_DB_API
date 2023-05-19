using LibraryFor_CAR_DB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _nameUser;
        Task taskVehicleDB;
        VehicleWindow vehicleWindow;
        CarOwnersWindow carOwnersWindow;
        DriverLicenseWindow driverLicenseWindow;
        VRCWindow vrcWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        public string? NameUser
        {
            get { return _nameUser; }
            set { this._nameUser = value; }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameUserLabel.Content = _nameUser;
            //StartAllDB();
            //WaitAll();
        }

        //public void StartAllDB()
        //{
        //    taskVehicleDB = Task.Factory.StartNew(() =>
        //    {
        //        vehicleWindow.getDB();
        //    }
        //    );
        //    taskCarOwnersDB = Task.Factory.StartNew(() =>
        //    {
        //        carOwnersWindow.getDB();
        //    }
        //    );
        //    taskDriversLicenseDB = Task.Factory.StartNew(() =>
        //    {
        //        driverLicenseWindow.getDB();
        //    }
        //   );
        //}

        //public void WaitAll()
        //{
        //    taskVehicleDB.Wait();
        //    taskCarOwnersDB.Wait();
        //    taskDriversLicenseDB.Wait();
        //}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            authorization auth = new authorization();
            auth.Show();
            this.Close();
        }

        private void VehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            vehicleWindow = new VehicleWindow();
            this.VehicleBtn.IsEnabled = false;
            vehicleWindow.Show();
            this.Close();
        }
        //CarOwnersWindow
        private void CarOwnersBtn_Click(object sender, RoutedEventArgs e)
        {
            carOwnersWindow = new CarOwnersWindow();
            this.OwnersBtn.IsEnabled = false;
            carOwnersWindow.Show();
            this.Close();
        }

        private void VRCBtn_Click(object sender, RoutedEventArgs e)
        {
            vrcWindow = new VRCWindow();
            vrcWindow.Show();
            this.Close();
        }

        private void DriversLicenseBtn_Click(object sender, RoutedEventArgs e)
        {
            driverLicenseWindow = new DriverLicenseWindow();
            this.DriversLicenseBtn.IsEnabled = false;
            driverLicenseWindow.Show();
            this.Close();
        }
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();
        }
        private void MinuteBtn_Click(object sender, RoutedEventArgs e)
        {
            MinuteWindow minuteWindow = new MinuteWindow();
            minuteWindow.Show();
            this.Close();
        }
    }
}
