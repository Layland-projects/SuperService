using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
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
    }
}
