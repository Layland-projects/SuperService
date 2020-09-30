using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class UserService
    {
        SuperServiceContext _db;
        public UserService(SuperServiceContext db)
        {
            _db = db;
        }
        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.Include(x => x.UserType);
        }

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
