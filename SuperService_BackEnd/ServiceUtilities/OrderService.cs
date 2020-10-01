using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
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

        public void AddNewOrder(Order order)
        {
            _db.Orders.Add(order);
            _db.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {
            _db.Orders.Remove(order);
            _db.SaveChanges();
        }

    }
}
