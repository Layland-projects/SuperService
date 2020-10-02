using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (oHelper.GetOrdersByTableNumber(1000).Count() > 0)
            {
                var oList = oHelper.GetOrdersByTableNumber(1000).ToList();
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
            if (oHelper.GetOrdersByTableNumber(1000).Count() > 0)
            {
                var oList = oHelper.GetOrdersByTableNumber(1000).ToList();
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
            Assert.IsTrue(oHelper.GetOrdersByTableNumber(1000).Count() > 0);
        }

        [Test]
        public void GetOrdersByTableNumberTest_WithTableNumberThatDoesntExist()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumber(int.MaxValue).Count() == 0);
        }

        [Test]
        public void GetOrderByOrderIDTest()
        {
            oHelper.AddNewOrder(order, items);
            foreach (var o in oHelper.GetOrdersByTableID(table.ID))
            {
                Assert.AreEqual(o.OrderID, oHelper.GetOrderByOrderID(o.OrderID).OrderID);
            }
        }

        [Test]
        public void GetOrderByOrderIDTest_WithIDDoesntExistInDB()
        {
            Assert.Null(oHelper.GetOrderByOrderID(int.MaxValue));
        }

        [Test]
        public void UpdateOrderStatusTest()
        {
            oHelper.AddNewOrder(order, items);
            var ord = oHelper.GetOrdersByTableID(table.ID);
            var newOrd = new List<Order>();
            foreach (var o in ord)
            {
                newOrd.Add(oHelper.UpdateOrderStatus(o, OrderStatusValues.InProcess));
            }
            foreach (var o in newOrd)
            {
                Assert.AreEqual(OrderStatusValues.InProcess.OrderStatusID, o.OrderStatusID);
            }
        }

        [Test]
        public void UpdateOrderStatusTest_WithOrderNotInDB()
        {
            Assert.Throws<ArgumentException>(() => oHelper.UpdateOrderStatus(order, OrderStatusValues.InProcess));
        }

        [Test]
        public void UpdateOrderStatusTest_WithCustomStatus()
        {
            oHelper.AddNewOrder(order, items);
            Assert.Throws<ArgumentException>(() => oHelper.UpdateOrderStatus(order, new OrderStatus { OrderStatusID = 12, Name = "A new Status", Description = "My test status" }));
        }

        [Test()]
        public void GetAllNoneCompletedOrdersTest()
        {
            oHelper.AddNewOrder(order, items);
            oHelper.AddNewOrder(new Order { Table = table }, items);
            Assert.IsTrue(oHelper.GetAllNoneCompletedOrders().Where(x => x.Table.TableNumber == table.TableNumber).Count() == 2);
            oHelper.UpdateOrderStatus(order, OrderStatusValues.Completed);
            Assert.IsTrue(oHelper.GetAllNoneCompletedOrders().Where(x => x.Table.TableNumber == table.TableNumber).Count() == 1);
        }
    }
}