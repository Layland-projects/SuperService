using SuperService_BackEnd.Models;
using System.Collections.Generic;

namespace SuperService_BackEnd.Interfaces
{
    public interface IOrderService
    {
        void AddNewOrder(Order order);
        void DeleteOrder(Order order);
        IEnumerable<Order> GetAllNoneCompletedOrders();
        Order GetOrderByOrderID(int id);
        IEnumerable<Order> GetOrdersByTableID(int id);
        IEnumerable<Order> GetOrdersByTableNumber(int number);
        void UpdateOrderStatus(Order order);
    }
}