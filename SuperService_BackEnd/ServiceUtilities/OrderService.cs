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
        public IEnumerable<Order> GetOrdersByTableNumber(int number)
        {
            using (var db = new SuperServiceContext())
            {
                return db.Orders.Include(x => x.Table).Where(x => x.Table.TableNumber == number).ToList();
            }
        }

        public Order GetOrderByOrderID(int id)
        {
            using (var db = new SuperServiceContext())
            {
                return db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Include(x => x.OrderStatus).Where(x => x.OrderID == id).FirstOrDefault();
            }
        }

        public IEnumerable<Order> GetOrdersByTableID(int id) 
        {
            using (var db = new SuperServiceContext())
            {
                return db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Include(x => x.OrderStatus).Where(x => x.Table.ID == id).ToList();
            }
        }

        public void AddNewOrder(Order order)
        {
            using (var db = new SuperServiceContext())
            {
                db.Attach(order);
                db.Entry(order).State = EntityState.Added;
                db.SaveChanges();
            }
        }
        public void DeleteOrder(Order order)
        {
            using (var db = new SuperServiceContext())
            {
                var ordersForId = db.Orders.Where(x => x.OrderID == order.OrderID).ToList();
                db.Orders.RemoveRange(ordersForId);
                db.SaveChanges();
            }
        }

        public IEnumerable<Order> GetAllNoneCompletedOrders()
        {
            using (var db = new SuperServiceContext())
            {
                return db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Where(x => x.OrderStatusID != OrderStatusService.Completed.OrderStatusID).ToList();
            }
        }

        public void UpdateOrderStatus(Order order)
        {
            using (var db = new SuperServiceContext())
            {
                var orderInDb = db.Orders.Where(x => x.OrderID == order.OrderID).FirstOrDefault();
                orderInDb.OrderStatus = order.OrderStatus;
                orderInDb.OrderStatusID = order.OrderStatusID;
                db.SaveChanges();
            }
        }
    }
}
