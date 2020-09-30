using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuperService_FrontEnd
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        User _user;
        UserHelper _uHelper;
        public ICollection<User> Users { get; private set; }
        public Login()
        {
            _uHelper = new UserHelper();
            DataContext = this;
            Users = _uHelper.GetAllUsers().ToList();
            InitializeComponent();
        }

        private void cbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;
            _user = (User)cb.SelectedItem;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
            {
                var main = new MainWindow(_user);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }
        }
    }
}
