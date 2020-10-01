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
        SuperServiceContext _db;
        public TableService(SuperServiceContext db)
        {
            _db = db;
        }
        public IEnumerable<Table> GetAllTables() => _db.Tables;
        public Table GetTableByID(int id) => _db.Tables.Where(x => x.ID == id).FirstOrDefault();
        public Table GetTableByTableNumber(int tableNumber) => _db.Tables.Where(x => x.TableNumber == tableNumber).FirstOrDefault();

        public void AddNewTable(Table table)
        {
            _db.Tables.Add(table);
            _db.SaveChanges();
        }

        public void DeleteTableByTableNumber(int number)
        {
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
