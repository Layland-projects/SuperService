using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class OrderHelper
    {
        OrderService _oService = new OrderService();
        OrderItemsService _oIService = new OrderItemsService();

        public Order GetOrderByOrderID(int id) => _oService.GetOrderByOrderID(id);
        public IEnumerable<Order> GetOrdersByTableNumber(int number) => _oService.GetOrdersByTableNumber(number);
        public IEnumerable<Order> GetOrdersByTableID(int id) => _oService.GetOrdersByTableID(id);
        public IEnumerable<Order> GetAllNoneCompletedOrders() => _oService.GetAllNoneCompletedOrders();

        public void AddNewOrder(Order order, IEnumerable<Item> items)
        {
            if (order.Table == null)
            {
                throw new ArgumentException("order must contain a Table");
            }
            if (items.Count() == 0)
            {
                throw new ArgumentException("items must contain at least one Item");
            }
            order.OrderStatusID = OrderStatusService.OrderPlaced.OrderStatusID;
            order.OrderStatus = null;
            _oService.AddNewOrder(order);
            List<OrderItems> orderItems = new List<OrderItems>();
            foreach (var item in items)
            {
                orderItems.Add(new OrderItems { ItemID = item.ItemID, OrderID = order.OrderID });
            }
            _oIService.AddNewOrderItems(orderItems);
        }

        public void DeleteOrder(Order order)
        {
            _oIService.DeleteOrderItemsByOrderID(order.OrderID);
            _oService.DeleteOrder(order);
        }

        public Order UpdateOrderStatus(Order order, OrderStatus status)
        {
            if (order == null || GetOrderByOrderID(order.OrderID) == null)
            {
                throw new ArgumentException("Provided order does not exist in the database, save the order before trying to update it's status");
            }
            else if (!OrderStatusValues.IsValidStatus(status))
            {
                throw new ArgumentException("Provided status does not exist in the database.");
            }
            order.OrderStatusID = status.OrderStatusID;
            order.OrderStatus = null;
            _oService.UpdateOrderStatus(order);
            order.OrderStatus = status;
            return order;
        }
    }
}
