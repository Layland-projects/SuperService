using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class UserTypeValues
    {
        public static UserType Admin => UserTypeService.Admin;
        public static UserType KitchenStaff => UserTypeService.KitchenStaff;
        public static UserType Server => UserTypeService.Server;
    }
}
