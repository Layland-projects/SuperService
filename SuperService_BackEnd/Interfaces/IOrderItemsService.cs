using SuperService_BackEnd.Models;
using System.Collections.Generic;

namespace SuperService_BackEnd.Interfaces
{
    public interface IOrderItemsService
    {
        void AddNewOrderItem(OrderItems orderItem);
        void AddNewOrderItems(IEnumerable<OrderItems> orderItems);
        void DeleteOrderItemsByOrderID(int orderID);
    }
}