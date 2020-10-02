using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using SuperService_BusinessLayer;
using SuperService_BusinessLayer.ViewModels;
using SuperService_FrontEnd.GUIHelpers;
using SuperService_FrontEnd.Windows;
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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page, INotifyPropertyChanged
    {
        OrderHelper _oHelper;
        ICollection<Order> _orderItemsNotWorked = new List<Order>();
        ICollection<OrderItemViewModel> _orderItemsInProgress = new List<OrderItemViewModel>();
        ICollection<OrderItemViewModel> _orderItemsReadyToCollect = new List<OrderItemViewModel>();
        List<Order> _ordersNotCompleted;

        public ICollection<Order> OrderItemsNotWorked
        {
            get => _orderItemsNotWorked;
            set
            {
                _orderItemsNotWorked = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public ICollection<OrderItemViewModel> OrderItemsInProgress
        {
            get => _orderItemsInProgress;
            set
            {
                _orderItemsInProgress = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public ICollection<OrderItemViewModel> OrderItemsReadyToCollect
        {
            get => _orderItemsReadyToCollect;
            set
            {
                _orderItemsReadyToCollect = value;
                Common.OnPropertyChanged(PropertyChanged, this);
            }
        }

        public Orders()
        {
            _oHelper = new OrderHelper();
            _ordersNotCompleted = _oHelper.GetAllNoneCompletedOrders().ToList();
            foreach (var order in _ordersNotCompleted)
            {
                if (order.OrderStatusID == OrderStatusValues.OrderPlaced.OrderStatusID)
                {
                    OrderItemsNotWorked.Add(order);
                    OrderItemsNotWorked = new List<Order>(OrderItemsNotWorked);
                }
            }
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
