
using System.Collections.Generic;
using System.Windows;
using staffDB;
using staffDB.Entity;
using staffDB.Services;
using System.Windows.Controls;
using System.Linq;

namespace RegistrationStaff
{
    /// <summary>
    /// Логика взаимодействия для PrintDatabase.xaml
    /// </summary>
    public partial class PrintDatabase : Window
    {
        Service_Staff service_Staff = new Service_Staff();
        List<Staff>? staffs = null;
        

        //Функции для окна
        public PrintDatabase()
        {
            InitializeComponent();
            printDB();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            printDB();
        }

        // Функции для работы с БД
        public void getDB()
        {
            staffs = service_Staff.getAllStaff();
        }
        public void printDB()
        {
            this.listView.DataContext = staffs;
            this.listView.ItemsSource = staffs;
        }

        // Функции для ListView
        private void listView_Selected(object sender, RoutedEventArgs e)
        {
            clearForm();
            //Console.WriteLine(this.listView.SelectedItem);
            Staff staff = (Staff)this.listView.SelectedItem;
            if (staff != null)
            {
                this.groupBoxID.Content = "ID: " + staff.Id;
                this.groupBoxFIO.Text = staff.Fio;
                this.groupBoxPass.Text = staff.Password;
                this.groupBtnClear.IsEnabled = true;
            }
          

        }

        //Функции для работы с формой
        private bool checkSameinDB()
        {
            Staff staff = new Staff();
            string Fio = this.groupBoxFIO.Text;
            string Password = this.groupBoxPass.Text;
            foreach (var item in staffs)
            {
                if (item.Fio == Fio & item.Password == Password)
                {
                    return false;
                }
            }
            return true;
        }

        private void clearForm()
        {
            this.groupBoxID.Content = "ID: ";
            this.groupBoxFIO.Text = "";
            this.groupBoxPass.Text = "";
            this.groupBtnClear.IsEnabled = false;
        }
        private void groupBoxsDataContextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.groupBoxPass.Text != "" & this.groupBoxFIO.Text != "" & checkSameinDB())
            {
                this.groupBtnAdd.IsEnabled = true;
                this.groupBtnSave.IsEnabled = true;
            }
            else
            {
                this.groupBtnAdd.IsEnabled = false;
                this.groupBtnSave.IsEnabled = false;
            }
        }

        // Обработка кнопок
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
            this.groupBtnClear.IsEnabled = false;
            this.groupBtnAdd.IsEnabled = false;
            this.groupBtnSave.IsEnabled = false;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = new Staff();
            staff.Fio = this.groupBoxFIO.Text;
            staff.Password = this.groupBoxPass.Text;
            string? id = this.groupBoxID.Content.ToString();
            id = id?.Substring(4);
            if(id != null)
            {
                service_Staff.edit(staff, int.Parse(id));
            }
            getDB();
            printDB();
            
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = new Staff();
            staff.Fio = this.groupBoxFIO.Text;
            staff.Password = this.groupBoxPass.Text;
            service_Staff.saveStaff(staff);
            getDB();
            printDB();
        }

        private void BtnYourButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext as Staff; 
            service_Staff.delete(item);
            getDB();
            printDB();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BoxChanged(object sender, TextChangedEventArgs e)
        {
            if (this.searchBox.Text == "")
            {
                printDB();
            }
            else
            {
                var list = (from item in staffs
                            where item.Fio.Contains(this.searchBox.Text)
                            select item);
                this.listView.ItemsSource = list;
            }
        }
    }
}
