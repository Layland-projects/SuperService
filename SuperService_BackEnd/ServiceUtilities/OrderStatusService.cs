using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public static class OrderStatusService
    {
        public static OrderStatus OrderPlaced 
        {
            get 
            { 
                using (var db = new SuperServiceContext())
                {
                    return db.OrderStatuses.Where(x => x.Name == "Order Placed").FirstOrDefault();
                }
            }  
        }
        public static OrderStatus InProcess
        {
            get
            {
                using (var db = new SuperServiceContext())
                {
                    return db.OrderStatuses.Where(x => x.Name == "In Process").FirstOrDefault();
                }
            }
        }
        public static OrderStatus ReadyToCollect
        {
            get
            {
                using (var db = new SuperServiceContext())
                {
                    return db.OrderStatuses.Where(x => x.Name == "Ready To Collect").FirstOrDefault();
                }
            }
        }
        public static OrderStatus Completed
        {
            get
            {
                using (var db = new SuperServiceContext())
                {
                    return db.OrderStatuses.Where(x => x.Name == "Completed").FirstOrDefault();
                }
            }
        }
    }
}
