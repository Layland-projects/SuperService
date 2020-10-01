using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class OrderService
    {
        SuperServiceContext _db;
        public OrderService(SuperServiceContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> GetOrdersByTableNumber(int number) => _db.Orders.Include(x => x.Table).Where(x => x.Table.TableNumber == number);
        public IEnumerable<Order> GetOrdersByTableID(int id) => _db.Orders.Include(x => x.Table).Where(x => x.Table.ID == id);

        public void AddNewOrder(Order order)
        {
            _db.Attach(order);
            _db.Entry(order).State = EntityState.Added;
            _db.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {
            var orderItemsForOrderID = _db.OrderItems.Include(x => x.Order).Where(x => x.Order.OrderID == order.OrderID).ToList();
            var ordersForId = _db.Orders.Where(x => x.OrderID == order.OrderID).ToList();
            _db.OrderItems.RemoveRange(orderItemsForOrderID);    
            _db.Orders.RemoveRange(order);
            _db.SaveChanges();  
        }

    }
}
