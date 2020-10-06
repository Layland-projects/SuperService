using NUnit.Framework;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer.Tests
{
    [TestFixture]
    public class UserHelperTests
    {
        [Test]
        public void GetAllUsersTest()
        {
            UserHelper uHelper = new UserHelper();
            Assert.IsTrue(uHelper.GetAllUsers().Count() > 0);
        }
    }
}