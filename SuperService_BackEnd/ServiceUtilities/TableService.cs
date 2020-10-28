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
        private SuperServiceContext _db;

        public TableService(SuperServiceContext db)
        {
            _db = db;
        }

        public IEnumerable<Table> GetAllTables()
        {
            return _db.Tables.ToList();
        }
        public Table GetTableByID(int id)
        {
            return _db.Tables.Where(x => x.ID == id).FirstOrDefault();
        }
        public Table GetTableByTableNumber(int tableNumber)
        {
            return _db.Tables.Where(x => x.TableNumber == tableNumber).FirstOrDefault();
        }

        public void AddNewTable(Table table)
        {
            _db.Tables.Add(table);
            _db.SaveChanges();
        }

        public void DeleteTableByTableNumber(int number)
        {
            var orderItemsForOrder = _db.OrderItems.Include(x => x.Order).ThenInclude(x => x.Table).Where(x => x.Order.Table.TableNumber == number).ToList();
            foreach(var entry in orderItemsForOrder)
            {
                _db.OrderItems.Remove(entry);
                _db.SaveChanges();
            }
            var ordersForTable = _db.Orders.Include(x => x.Table).Where(x => x.Table.TableNumber == number).ToList();
            foreach (var entry in ordersForTable)
            {
                _db.Orders.Remove(entry);
                _db.SaveChanges();
            }
            _db.Tables.RemoveRange(_db.Tables.Where(x => x.TableNumber == number));
            _db.SaveChanges();
        }
    }
}
