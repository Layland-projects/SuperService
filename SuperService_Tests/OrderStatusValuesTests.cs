using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer.Tests
{
    [TestFixture]
    public class OrderStatusValuesTests
    {
        [Test]
        public void IsValidStatusTest()
        {
            Assert.IsTrue(OrderStatusValues.IsValidStatus(OrderStatusValues.InProcess));
            Assert.IsFalse(OrderStatusValues.IsValidStatus(new OrderStatus { OrderStatusID = int.MinValue, Name = "Test status", Description = "My test status" }));
        }
    }
}