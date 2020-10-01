using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using SuperService_FrontEnd.GUIHelpers;
using SuperService_FrontEnd.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SuperService_FrontEnd.Pages
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page, INotifyPropertyChanged
    {
        bool _isBuildingOrder;
        Order _newOrder;
        ItemHelper _iHelper;
        TableHelper _tHelper;
        ICollection<Item> _itemsOrdered;
        public ICollection<Item> MenuItems { get; private set; }
        public ICollection<SuperService_BackEnd.Models.Table> Tables { get; set; }
        public ICollection<Item> ItemsOrdered
        {
            get => _itemsOrdered;
            private set
            {
                _itemsOrdered = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public NewOrder()
        {
            _newOrder = new Order();
            _iHelper = new ItemHelper();
            _tHelper = new TableHelper();
            MenuItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
            ItemsOrdered = new List<Item>();
            Tables = _tHelper.GetAllTablesOrderedByNumber().ToList();
            InitializeComponent();
            DataContext = this;
        }
        private void Menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemToAdd = (Item)((ListView)sender).SelectedItem;
            ItemsOrdered.Add(itemToAdd);
            ItemsOrdered = new List<Item>(ItemsOrdered);
            _isBuildingOrder = true;
            ToggleEditMode();
        }
        private void Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemToRemove = (Item)((ListView)sender).SelectedItem;
            ItemsOrdered.Remove(itemToRemove);
            ItemsOrdered = new List<Item>(ItemsOrdered);
            if (ItemsOrdered.Count == 0)
            {
                _isBuildingOrder = false;
                ToggleEditMode();
            }
        }
        private void cbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _newOrder.Table = (SuperService_BackEnd.Models.Table)((ComboBox)sender).SelectedItem;
        }


        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleEditMode()
        {
            if (_isBuildingOrder)
            {
                btnPlaceOrder.IsEnabled = true;
                btnUndo.IsEnabled = true;
                ((MainWindow)App.Current.MainWindow).btnStock.IsEnabled = false;
                ((MainWindow)App.Current.MainWindow).btnLogout.IsEnabled = false;
                ((MainWindow)App.Current.MainWindow).btnOrders.IsEnabled = false;
            }
            else
            {
                btnPlaceOrder.IsEnabled = false;
                btnUndo.IsEnabled = false;
                ((MainWindow)App.Current.MainWindow).btnStock.IsEnabled = true;
                ((MainWindow)App.Current.MainWindow).btnLogout.IsEnabled = true;
                ((MainWindow)App.Current.MainWindow).btnOrders.IsEnabled = true;
            }
        }
    }
}
