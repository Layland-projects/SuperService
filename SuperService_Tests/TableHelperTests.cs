using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer.Tests
{
    [TestFixture]
    public class TableHelperTests
    {
        TableHelper tHelper = new TableHelper();
        [Test]
        public void GetAllTablesOrderedByNumberTest()
        {
            tHelper.AddNewTable(new Table { NumberOfSeats = 1, TableNumber = 1000 });
            tHelper.AddNewTable(new Table { NumberOfSeats = 1, TableNumber = 1 });
            var tables = tHelper.GetAllTablesOrderedByNumber().ToList();

            Assert.IsTrue(tables.First().TableNumber < tables.Last().TableNumber);
        }
    }
}