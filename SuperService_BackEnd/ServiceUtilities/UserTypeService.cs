using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class UserTypeService
    {
        public static UserType Admin
        {
            get 
            {
                using(var db = new SuperServiceContext())
                {
                    return db.UserTypes.Where(x => x.Name == "Admin").FirstOrDefault();
                }
            }
        }

        public static UserType KitchenStaff
        {
            get
            {
                using (var db = new SuperServiceContext())
                {
                    return db.UserTypes.Where(x => x.Name == "Kitchen staff").FirstOrDefault();
                }
            }
        }

        public static UserType Server
        {
            get
            {
                using (var db = new SuperServiceContext())
                {
                    return db.UserTypes.Where(x => x.Name == "Server").FirstOrDefault();
                }
            }
        }
    }
}
