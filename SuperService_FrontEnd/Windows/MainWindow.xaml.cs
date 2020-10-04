using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using SuperService_FrontEnd.GUIHelpers;
using SuperService_FrontEnd.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperService_FrontEnd.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User _loggedInUser;
        public string WelcomeMessage { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User user)
        {
            _loggedInUser = user;
            WelcomeMessage = $"Welcome back, {user.GetFullName()}";
            DataContext = this;
            InitializeComponent();
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.KitchenStaff.UserTypeID)
            {
                btnNewOrder.Visibility = Visibility.Visible;
                //btnNewOrder.Visibility = Visibility.Visible;
            }
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.Server.UserTypeID) 
            {
                btnStock.Visibility = Visibility.Visible;
            }
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            buttonClickLockManager((Button)sender);
            _frame.Navigate(new Stock());
        }
        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            buttonClickLockManager((Button)sender);
            _frame.Navigate(new NewOrder());
        }
        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            buttonClickLockManager((Button)sender);
            _frame.Navigate(new Orders(_loggedInUser));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            App.Current.MainWindow = login;
            this.Close();
            login.Show();
        }
        public void buttonClickLockManager(Button buttonClicked)
        {
            buttonClicked.IsEnabled = false;
            foreach (var button in Common.GetAllButtonsInWindow(this))
            {
                if (button != buttonClicked)
                {
                    button.IsEnabled = true;
                }
            }
        }

    }
}
