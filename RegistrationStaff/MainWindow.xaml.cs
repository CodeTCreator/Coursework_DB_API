using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RegistrationStaff
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PrintDatabase printDatabase;
        Task taskDB;
        public MainWindow()
        {
            InitializeComponent();
            StartDB();
        }


        private void StartDB()
        {
            printDatabase = new PrintDatabase();
            
            taskDB = Task.Factory.StartNew(() =>
            {
                printDatabase.getDB();
            }
            );
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           

            if (this.Login.Text == "admin" && this.password.Password.ToString() == "admin")
            {
                taskDB.Wait();
                this.enterBtn.IsEnabled = false;
                printDatabase.Show();
                this.Close();
               
               
            }
            else
            {
                string messageBoxText = "Неправильный логин или пароль!";
                string caption = "Ошибка!";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult messageBox;
                messageBox = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            }

        }
    }
}
