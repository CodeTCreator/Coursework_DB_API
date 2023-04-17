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
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            
            authorization auth = new authorization();
            auth.Show();
            this.Close();
        }

    }
}
