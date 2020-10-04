using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using SuperService_FrontEnd.GUIHelpers;
using SuperService_FrontEnd.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        OrderHelper _oHelper;
        ItemHelper _iHelper;
        TableHelper _tHelper;
        List<Item> _itemsOrdered;
        List<Item> _historicItems;
        string _totalCost = 0.ToString("C");
        public ICollection<Item> MenuItems { get; private set; }
        public ICollection<SuperService_BackEnd.Models.Table> Tables { get; set; }
        public List<Item> ItemsOrdered
        {
            get => _itemsOrdered;
            private set
            {
                _itemsOrdered = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        } 
        public string TotalCost
        {
            get => _totalCost;
            private set
            {
                _totalCost = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public NewOrder()
        {
            _newOrder = new Order();
            _iHelper = new ItemHelper();
            _tHelper = new TableHelper();
            _oHelper = new OrderHelper();
            MenuItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
            _historicItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
            ItemsOrdered = new List<Item>();
            Tables = _tHelper.GetAllTablesOrderedByNumber().ToList();
            InitializeComponent();
            DataContext = this;
        }
        private void Menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemToAdd = (Item)((ListView)sender).SelectedItem;
            if (itemToAdd != null && itemToAdd.CanOrder)
            {
                ItemsOrdered.Add(itemToAdd);
                ItemsOrdered = new List<Item>(ItemsOrdered);
                _iHelper.DecremenetStockForItem(itemToAdd);
                _isBuildingOrder = true;
                ToggleEditMode();
                CalculateTotal();
                MenuItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
                Menu.ItemsSource = MenuItems;
            }
        }
        private void Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var itemToRemove = (Item)((ListView)sender).SelectedItem;
            ItemsOrdered.Remove(itemToRemove);
            ItemsOrdered = new List<Item>(ItemsOrdered);
            _iHelper.IncrementStockForItem(itemToRemove);
            if (ItemsOrdered.Count == 0)
            {
                _isBuildingOrder = false;
                ToggleEditMode();
            }
            CalculateTotal();
            MenuItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
            Menu.ItemsSource = MenuItems;
        }
        private void cbTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _newOrder.Table = (SuperService_BackEnd.Models.Table)((ComboBox)sender).SelectedItem;
        }

        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_newOrder.Table == null)
            {
                MessageBox.Show("Please select a table", "Warning");
            }
            else
            {
                _oHelper.AddNewOrder(_newOrder, ItemsOrdered);
                MessageBox.Show("Order placed!", "Success");
                _isBuildingOrder = false;
                ClearSelection();
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            _isBuildingOrder = false;
            ClearSelection();
        }

        private void DisplayItemInfo(object sender, RoutedEventArgs e)
        {
            var item = (Item)((ListView)sender).SelectedItem;
            if (item != null)
            {
                string message = $"{item.Name}\n" +
                    $"\tCalories: {item.FormattedCalories}\n" +
                    $"\tProtein: {item.FormattedProtein}\n" +
                    $"\tFat: {item.FormattedFat}\n" +
                    $"\tCarbs: {item.FormattedCarbs}\n" +
                    $"\tSugar: {item.FormattedSugar}\n" +
                    $"\tSalt: {item.FormattedSalt}\n" +
                    $"Ingredients:\n";
                foreach (var itemIngredient in item.ItemIngredients)
                {
                    var name = itemIngredient.Ingredient.Name;
                    if (message.Contains(name))
                    {
                        if (int.TryParse(message[message.IndexOf(name) - 3].ToString(), out int multiplier))
                        {
                            multiplier++;
                            message = message.Substring(0, message.IndexOf(name) - 3) +  message.Substring(message.IndexOf(name) - 3, name.Length + 3).Replace((multiplier - 1).ToString(), multiplier++.ToString()) + message.Substring(message.IndexOf(name) + name.Length);
                        }
                        else
                        {
                            message = message.Insert(message.IndexOf(name), "2x ");
                        }
                    }
                    else
                    {
                        message += $"\t{name}\n";
                    }
                }
                MessageBox.Show(message, $"{item.Name}");
            }
        }

        private void ClearSelection()
        {
            cbTables.SelectedItem = null;
            Menu.SelectedItem = null;
            while(ItemsOrdered.Count > 0)
            {
                _iHelper.IncrementStockForItem(ItemsOrdered[0]);
                ItemsOrdered.Remove(ItemsOrdered[0]);
                ItemsOrdered = new List<Item>(ItemsOrdered);
            }
            MenuItems = _iHelper.GetAllItemsOrderedByAvailability().ToList();
            Menu.ItemsSource = MenuItems;
            ToggleEditMode();
        }

        private void CalculateTotal()
        {
            TotalCost = ItemsOrdered.Select(x => x.Cost).Sum().ToString("C", CultureInfo.CurrentCulture);
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
                CalculateTotal();
            }
        }
    }
}
