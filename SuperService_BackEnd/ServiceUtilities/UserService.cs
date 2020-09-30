using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class UserService
    {
        public User Login(string username, string password)
        {
            using (var db = new SuperServiceContext())
            {
                //Add password hashing encryption/decryption
                return db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            }
        }
    }
}
