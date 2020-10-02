using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
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
            if (oHelper.GetOrdersByTableNumberList(1000).Count() > 0)
            {
                var oList = oHelper.GetOrdersByTableNumberList(1000).ToList();
                foreach (var o in oList)
                {
                    oHelper.DeleteOrder(o);
                }
            }
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
            if (oHelper.GetOrdersByTableNumberList(1000).Count() > 0)
            {
                var oList = oHelper.GetOrdersByTableNumberList(1000).ToList();
                foreach (var o in oList)
                {
                    oHelper.DeleteOrder(o);
                }
            }
            if (tHelper.GetTableByTableNumber(1000) != null)
            {
                tHelper.DeleteTableByTableNumber(1000);
            }
        }

        [Test]
        public void AddNewOrderTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 1);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Where(x => x.OrderStatus.OrderStatusID == OrderStatusService.OrderPlaced.OrderStatusID).Count() == 1);
        }

        [Test]
        public void AddNewOrderTest_WithEmptyOrder()
        {
            Assert.Throws<ArgumentException>(() => oHelper.AddNewOrder(new Order(), items));
        }

        [Test]
        public void AddNewOrderTest_WithNoItems()
        {
            Assert.Throws<ArgumentException>(() => oHelper.AddNewOrder(order, new List<Item>()));
        }

        [Test]
        public void DeleteOrderTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 1);
            oHelper.DeleteOrder(order);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 0);
        }

        [Test]
        public void DeleteOrderTest_WithOrderThatDoesntExist()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 1);
            oHelper.DeleteOrder(new Order { OrderID = int.MaxValue });
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 1);
        }

        [Test]
        public void GetOrdersByTableNumberTest()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumberList(1000).Count() > 0);
        }

        [Test]
        public void GetOrdersByTableNumberTest_WithTableNumberThatDoesntExist()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumberList(int.MaxValue).Count() == 0);
        }
    }
}