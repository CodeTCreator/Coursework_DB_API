
using System.Windows;
using System.Windows.Controls;
using staffDB;
using staffDB.Services;

namespace RegistrationStaff
{
    /// <summary>
    /// Логика взаимодействия для PrintDatabase.xaml
    /// </summary>
    public partial class PrintDatabase : Window
    {
        public PrintDatabase()
        {
            InitializeComponent();

            ListView listView = this.listViewDB;
           
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            //this.Close();
            Service_Staff service_Staff = new Service_Staff();
            //var staffs = service_Staff.getAllStaff();
            //foreach(var item in staffs)
            //{
            //    listView.Items.Add(item);
            //}
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
