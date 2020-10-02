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
        public void AddNewOrderItem(OrderItems orderItem)
        {
            using (var db = new SuperServiceContext())
            {
                db.OrderItems.Attach(orderItem);
                db.Entry(orderItem).State = EntityState.Added;
                db.SaveChanges();
            }
        }
        public void AddNewOrderItems(IEnumerable<OrderItems> orderItems)
        {
            using (var db = new SuperServiceContext())
            {
                foreach (var item in orderItems)
                {
                    AddNewOrderItem(item);
                    //db.OrderItems.Attach(item);
                    //db.Entry(item).State = EntityState.Added;
                    //db.SaveChanges();
                }

            }

        }

        public void DeleteOrderItemsByOrderID(int orderID)
        {
            using (var db = new SuperServiceContext())
            {
                db.RemoveRange(db.OrderItems.Include(x => x.Order).Where(x => x.Order.OrderID == orderID));
                db.SaveChanges();
            }
        }
    }
}
