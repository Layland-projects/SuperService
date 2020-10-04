using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using SuperService_BusinessLayer;
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
    public partial class Orders : Page
    {
        OrderHelper _oHelper;
        User _loggedInUser;
        List<Order> _ordersNotCompleted;

        public ICollection<Order> OrderItemsNotWorked { get; private set; }

        public ICollection<Order> OrderItemsInProgress { get; private set; }

        public ICollection<Order> OrderItemsReadyToCollect { get; private set; }

        public Orders(User user)
        {
            _loggedInUser = user;
            _oHelper = new OrderHelper();
            InitializeComponent();
            DataContext = this;
            AttachSources();
        }

        private void OrderPlaced_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.Server.UserTypeID)
            {
                var order = (Order)((ListView)sender).SelectedItem;
                _oHelper.UpdateOrderStatus(order, OrderStatusValues.InProcess);
                AttachSources();
            }
        }

        private void InProgress_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.Server.UserTypeID)
            {
                var order = (Order)((ListView)sender).SelectedItem;
                _oHelper.UpdateOrderStatus(order, OrderStatusValues.ReadyToCollect);
                AttachSources();
            }
        }
        private void InProgress_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.Server.UserTypeID)
            {
                var order = (Order)((ListView)sender).SelectedItem;
                _oHelper.UpdateOrderStatus(order, OrderStatusValues.OrderPlaced);
                AttachSources();
            }
        }
        private void ReadyToCollect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.KitchenStaff.UserTypeID)
            {
                var order = (Order)((ListView)sender).SelectedItem;
                _oHelper.UpdateOrderStatus(order, OrderStatusValues.Completed);
                AttachSources();
            }
        }

        private void ReadyToCollect_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_loggedInUser.UserType.UserTypeID != UserTypeValues.Server.UserTypeID)
            {
                var order = (Order)((ListView)sender).SelectedItem;
                _oHelper.UpdateOrderStatus(order, OrderStatusValues.InProcess);
                AttachSources();
            }
        }
        public void AttachSources()
        {
            OrderItemsNotWorked = new List<Order>();
            OrderItemsInProgress = new List<Order>();
            OrderItemsReadyToCollect = new List<Order>();
            _ordersNotCompleted = _oHelper.GetAllNoneCompletedOrders().ToList();
            foreach (var order in _ordersNotCompleted)
            {
                if (order.OrderStatusID == OrderStatusValues.OrderPlaced.OrderStatusID)
                {
                    OrderItemsNotWorked.Add(order);
                }
                if (order.OrderStatusID == OrderStatusValues.InProcess.OrderStatusID)
                {
                    OrderItemsInProgress.Add(order);
                }
                if (order.OrderStatusID == OrderStatusValues.ReadyToCollect.OrderStatusID)
                {
                    OrderItemsReadyToCollect.Add(order);
                }
            }
            OrderPlaced.ItemsSource = OrderItemsNotWorked;
            InProgress.ItemsSource = OrderItemsInProgress;
            ReadyToCollect.ItemsSource = OrderItemsReadyToCollect;
            SetupGroups(OrderPlaced);
            SetupGroups(InProgress);
            SetupGroups(ReadyToCollect);
        }
        public void SetupGroups(ItemsControl control)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(control.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Table.DisplayTableNumber");
            view.GroupDescriptions.Add(groupDescription);
            groupDescription = new PropertyGroupDescription("DisplayOrderID");
            view.GroupDescriptions.Add(groupDescription);
        }

    }
}
