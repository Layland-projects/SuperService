using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SuperService_BackEnd;
using SuperService_BackEnd.Interfaces;
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
        SuperServiceContext db;
        ItemHelper iHelper;
        TableHelper tHelper;
        OrderHelper oHelper;
        Table table;
        Order order;
        List<Item> items;

        [SetUp]
        public void SetUp()
        {
            db = new SuperServiceContext(new DbContextOptionsBuilder().UseInMemoryDatabase(databaseName: "Fake_DB").Options);
            iHelper = new ItemHelper(db);
            tHelper = new TableHelper(db);
            oHelper = new OrderHelper(new OrderService(db), new OrderItemsService(db));
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
            if (iHelper.GetAllItemsOrderedByAvailability().FirstOrDefault() == null)
            {
                iHelper.AddNewItem(new Item { Cost = 5, Name = "TestSandwich" }, new List<Ingredient>
                {
                new Ingredient { Name = "TestBread", Calories = 230, Carbohydrates = 15, Fat = 1, Protein = 1, Salt = 5, Sugar = 5, NumberInStock = 10 },
                new Ingredient { Name = "TestCheese", Calories = 150, Carbohydrates = 1, Fat = 10, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 10 }
                });
                iHelper.AddNewItem(new Item { Cost = 8, Name = "TestStew" }, new List<Ingredient>
                {
                new Ingredient { Name = "TestCabbage", Calories = 60, Carbohydrates = 15, Fat = 1, Protein = 1, Salt = 5, Sugar = 5, NumberInStock = 10 },
                new Ingredient { Name = "TestPotatoes", Calories = 300, Carbohydrates = 60, Fat = 3, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 10 },
                new Ingredient { Name = "TestCarrots", Calories = 100, Carbohydrates = 20, Fat = 10, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 0 },
                });
            }
            
            items = new List<Item>
            {
                iHelper.GetAllItemsOrderedByAvailability().First(),
                iHelper.GetAllItemsOrderedByAvailability().Last()
            };
            db.OrderStatuses.Add(new OrderStatus() { Name = "Order Placed" });
            db.SaveChanges();
        }
        [TearDown]
        public void TearDown()
        {
            db.Dispose();
        }

        [Test]
        public void AddNewOrder_CreatesNewOrderInOrdersTable()
        {
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableID(table.ID).Count() == 1);
        }

        [Test]
        public void AddNewOrder_UpdatesOrdersStatusToOrderPlaced()
        {
            var oService = new Mock<IOrderService>();
            var oIService = new Mock<IOrderItemsService>();
            oHelper = new OrderHelper(oService.Object, oIService.Object);
            oHelper.AddNewOrder(order, items);
            Assert.That(order.OrderStatusID, Is.EqualTo(1));
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
            var oService = new Mock<IOrderService>(MockBehavior.Strict);
            var oIService = new Mock<IOrderItemsService>();
            oService.Setup(x => x.GetOrdersByTableNumber(1000)).Returns(new List<Order> { new Order { OrderID = 1, OrderStatusID = 1, Table = table } });
            oService.Setup(x => x.AddNewOrder(order));
            oHelper = new OrderHelper(oService.Object, oIService.Object);
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumber(1000).Count() > 0);
        }

        [Test]
        public void GetOrdersByTableNumberTest_WithTableNumberThatDoesntExist()
        {
            var oService = new Mock<IOrderService>();
            var oIService = new Mock<IOrderItemsService>();
            oService.Setup(x => x.GetOrdersByTableNumber(int.MinValue)).Returns(new List<Order>());
            oHelper = new OrderHelper(oService.Object, oIService.Object);
            oHelper.AddNewOrder(order, items);
            Assert.IsTrue(oHelper.GetOrdersByTableNumber(int.MinValue).Count() == 0);
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
        public void GetOrderByOrderIDTest_ReturnsNullWhenTimeout()
        {
            var oService = new Mock<IOrderService>();
            var oIService = new Mock<IOrderItemsService>();
            oService.Setup(x => x.GetOrderByOrderID(It.IsAny<int>())).Throws(new TimeoutException());
            oHelper = new OrderHelper(oService.Object, oIService.Object);
            Assert.That(oHelper.GetOrderByOrderID(1), Is.Null);
            //is it possible to test that a return is an instance of an object, WITH default properties?
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