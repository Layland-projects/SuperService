using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class TableHelper
    {
        TableService _serv = new TableService(new SuperServiceContext());
        public IEnumerable<Table> GetAllTablesOrderedByNumber() => _serv.GetAllTables().OrderBy(x => x.TableNumber);
    }
}
