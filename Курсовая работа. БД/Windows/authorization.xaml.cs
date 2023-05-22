using staffDB.Entity;
using staffDB.Services;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows;
using Курсовая_работа._БД.Box;

namespace Курсовая_работа._БД.Windows
{
    /// <summary>
    /// Логика взаимодействия для authorization.xaml
    /// </summary>
    public partial class authorization : Window
    {
        Service_Staff service_Staff = new Service_Staff();
        public authorization()
        {
            InitializeComponent();
        }

        private void enterButton_click(object sender, RoutedEventArgs e)
        {
            Staff staff = new Staff();
            staff.Fio = this.LoginTextBox.Text;
            staff.Password = this.PasswordBox.Password.ToString();
           
            var outer = Task.Factory.StartNew(() =>      // внешняя задача
            {
                BoxVRC boxVrc = new BoxVRC();
                boxVrc.GetVRC();
            });

            if (service_Staff.getStaff(staff) != null)
            {
                outer.Wait();
                MainWindow mainWindow = new MainWindow();
                mainWindow.NameUser = staff.Fio;
                mainWindow.Show();
                this.Close();
            }
            else
            {
                outer.Wait();
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
