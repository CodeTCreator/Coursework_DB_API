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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Курсовая_работа._БД
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _nameUser;
        Task taskVehicleDB;
        Task taskCarOwnersDB;
        VehicleWindow vehicleWindow;
        CarOwnersWindow carOwnersWindow;

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
            StartAllDB();
        }

        private void StartAllDB()
        {
            vehicleWindow = new VehicleWindow();
            carOwnersWindow = new CarOwnersWindow();
            taskVehicleDB = Task.Factory.StartNew(() =>
            {
                vehicleWindow.getDB();
            }
            );
            taskCarOwnersDB = Task.Factory.StartNew(() =>
            {
                carOwnersWindow.getDB();
            }
            );
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            authorization auth = new authorization();
            auth.Show();
            this.Close();
        }

        private void VehicleBtn_Click(object sender, RoutedEventArgs e)
        {
            taskVehicleDB.Wait();
            this.VehicleBtn.IsEnabled = false;
            vehicleWindow.Show();
            this.Close();
        }
        //CarOwnersWindow
        private void CarOwnersBtn_Click(object sender, RoutedEventArgs e)
        {
            taskCarOwnersDB.Wait();
            this.OwnersBtn.IsEnabled = false;
            carOwnersWindow.Show();
            this.Close();
        }
    }
}
