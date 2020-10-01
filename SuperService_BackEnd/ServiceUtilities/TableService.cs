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

        public void DeleteTableByTableID(int number)
        {
            _db.Tables.RemoveRange(_db.Tables.Where(x => x.TableNumber == number));
            _db.SaveChanges();
        }
    }
}
