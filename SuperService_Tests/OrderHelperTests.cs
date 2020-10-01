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
    public class OrderHelperTests
    {
        ItemHelper iHelper = new ItemHelper();
        TableHelper tHelper = new TableHelper();
        OrderHelper oHelper = new OrderHelper();
        Table table;
        Order order;
        List<Item> items;

        [SetUp]
        public void SetUp()
        {
            //if (oHelper.GetOrdersByTableNumber(1000).Count() > 0)
            //{
            //    var oList = oHelper.GetOrdersByTableNumber(1000).ToList();
            //    foreach (var o in oList)
            //    {
            //        oHelper.DeleteOrder(o);
            //    }
            //}
            if (tHelper.GetTableByTableNumber(1000) != null)
            {
                tHelper.DeleteTableByTableNumber(1000);
            }
            tHelper.AddNewTable(new Table
            {
                TableNumber = 1000,
                NumberOfSeats = 1,
            });
            table = tHelper.GetTableByTableNumber(1000);
            order = new Order { Table = table };
            items = new List<Item>
            {
                iHelper.GetAllItemsOrderedByAvailability().First(),
                iHelper.GetAllItemsOrderedByAvailability().First(),
                iHelper.GetAllItemsOrderedByAvailability().First()
            };
        }
        [TearDown]
        public void TearDown()
        {
            //if (oHelper.GetOrdersByTableNumber(1000).Count() > 0)
            //{
            //    var oList = oHelper.GetOrdersByTableNumber(1000).ToList();
            //    foreach(var o in oList)
            //    {
            //        oHelper.DeleteOrder(o);
            //    }
            //}
            if (tHelper.GetTableByTableNumber(1000) != null)
            {
                tHelper.DeleteTableByTableNumber(1000);
            }
        }

        [Test]
        public void AddNewOrderTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrderByTableID(table.ID).Count() == 1);
        }

        [Test]
        public void DeleteOrderTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrderByTableID(table.ID).Count() == 1);
            oHelper.DeleteOrder(order); // for some reason this stops me from being able to add more orderItems, need to figure out why
            Assert.IsTrue(oHelper.GetOrderByTableID(table.ID).Count() == 0);
        }

        [Test]
        public void GetOrdersByTableNumberTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumber(1000).Count() > 0);
        }
    }
}