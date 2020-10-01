using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using SuperService_FrontEnd.GUIHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace SuperService_FrontEnd.Pages
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page, INotifyPropertyChanged
    {
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
        public NewOrder()
        {
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
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
