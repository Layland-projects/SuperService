using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class OrderItemsService
    {
        SuperServiceContext _db;
        public OrderItemsService(SuperServiceContext db)
        {
            _db = db;
        }
        public void AddNewOrderItem(OrderItems orderItem)
        {
            _db.OrderItems.Add(orderItem);
            _db.SaveChanges();
        }
        public void AddNewOrderItems(IEnumerable<OrderItems> orderItems)
        {

            foreach (var item in orderItems)
            {
                _db.Attach(item);
                _db.Entry(item).State = EntityState.Added;
                _db.SaveChanges();
            }

        }

        public void DeleteOrderItemsByOrderID(int orderID)
        {
            _db.RemoveRange(_db.OrderItems.Include(x => x.Order).Where(x => x.Order.OrderID == orderID));
            _db.SaveChanges();
        }
    }
}
