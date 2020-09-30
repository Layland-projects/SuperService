using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;

namespace SuperService_BusinessLayer
{
    public class UserHelper
    {
        UserService _serv = new UserService(new SuperServiceContext());
        public IEnumerable<User> GetAllUsers()
        {
            return _serv.GetAllUsers();
        }
    }
}
