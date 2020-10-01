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

        public IEnumerable<Order> GetOrdersByTableNumberList(int number) => _oService.GetOrdersByTableNumber(number);
        public IEnumerable<Order> GetOrderByTableIDList(int id) => _oService.GetOrdersByTableID(id);

        public void AddNewOrder(Order order, IEnumerable<Item> items)
        {
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
    }
}
