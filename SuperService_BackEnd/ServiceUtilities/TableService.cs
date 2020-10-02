using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class TableService
    {
        public IEnumerable<Table> GetAllTables()
        {
            using (var db = new SuperServiceContext())
            {
                return db.Tables.ToList();
            }
        }
        public Table GetTableByID(int id)
        {
            using (var db = new SuperServiceContext())
            {
                return db.Tables.Where(x => x.ID == id).FirstOrDefault();
            }
        }
        public Table GetTableByTableNumber(int tableNumber)
        {
            using (var db = new SuperServiceContext())
            {
                return db.Tables.Where(x => x.TableNumber == tableNumber).FirstOrDefault();
            }
        }

        public void AddNewTable(Table table)
        {
            using (var db = new SuperServiceContext())
            {
                db.Tables.Add(table);
                db.SaveChanges();
            }
        }

        public void DeleteTableByTableNumber(int number)
        {
            using (var db = new SuperServiceContext())
            {
                var orderItemsForOrder = db.OrderItems.Include(x => x.Order).ThenInclude(x => x.Table).Where(x => x.Order.Table.TableNumber == number).ToList();
                foreach(var entry in orderItemsForOrder)
                {
                    db.OrderItems.Remove(entry);
                    db.SaveChanges();
                }
                var ordersForTable = db.Orders.Include(x => x.Table).Where(x => x.Table.TableNumber == number).ToList();
                foreach (var entry in ordersForTable)
                {
                    db.Orders.Remove(entry);
                    db.SaveChanges();
                }
                db.Tables.RemoveRange(db.Tables.Where(x => x.TableNumber == number));
                db.SaveChanges();
            }
        }
    }
}
