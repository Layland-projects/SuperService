using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Interfaces;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class OrderService : IOrderService
    {
        SuperServiceContext _db = new SuperServiceContext();

        public OrderService() { }
        public OrderService(SuperServiceContext db)
        {
            _db = db;
        }

        public IEnumerable<Order> GetOrdersByTableNumber(int number)
        {
            return _db.Orders.Include(x => x.Table).Where(x => x.Table.TableNumber == number).AsNoTracking().ToList();
        }

        public Order GetOrderByOrderID(int id)
        {
            return _db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Include(x => x.OrderStatus).Where(x => x.OrderID == id).AsNoTracking().FirstOrDefault();
        }

        public IEnumerable<Order> GetOrdersByTableID(int id)
        {
            return _db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Include(x => x.OrderStatus).Where(x => x.Table.ID == id).AsNoTracking().ToList();
        }

        public void AddNewOrder(Order order)
        {
            _db.Attach(order);
            _db.Entry(order).State = EntityState.Added;
            _db.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {

            var ordersForId = _db.Orders.Where(x => x.OrderID == order.OrderID).ToList();
            _db.Orders.RemoveRange(ordersForId);
            _db.SaveChanges();

        }

        public IEnumerable<Order> GetAllNoneCompletedOrders()
        {
            return _db.Orders.Include(x => x.Table).Include(x => x.Items).ThenInclude(x => x.Item).ThenInclude(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Where(x => x.OrderStatusID != OrderStatusService.Completed.OrderStatusID).AsNoTracking().ToList();
        }

        public void UpdateOrderStatus(Order order)
        {
            var orderInDb = _db.Orders.Where(x => x.OrderID == order.OrderID).FirstOrDefault();
            orderInDb.OrderStatus = order.OrderStatus;
            orderInDb.OrderStatusID = order.OrderStatusID;
            _db.SaveChanges();
        }
    }
}
