using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class OrderHelper
    {
        OrderService _oService = new OrderService(new SuperServiceContext());
        OrderItemsService _oIService = new OrderItemsService(new SuperServiceContext());

        public IEnumerable<Order> GetOrdersByTableID(int number) => _oService.GetOrdersByTableNumber(number);

        public void AddNewOrder(Order order, IEnumerable<Item> items)
        {
            List<OrderItems> orderItems = new List<OrderItems>();
            foreach (var item in items)
            {
                orderItems.Add(new OrderItems { Item = item, Order = order });
            }
            _oIService.AddNewOrderItems(orderItems);
        }

        public void DeleteOrder(Order order)
        {
            _oIService.DeleteOrderItemsByOrderID(order.OrderID);
            _oService.DeleteOrder(order);
        }
    }
}
