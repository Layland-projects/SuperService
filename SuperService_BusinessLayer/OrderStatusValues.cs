using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer
{
    public static class OrderStatusValues
    {
        public static OrderStatus OrderPlaced => OrderStatusService.OrderPlaced;
        public static OrderStatus InProcess => OrderStatusService.InProcess;
        public static OrderStatus ReadyToCollect => OrderStatusService.ReadyToCollect;
        public static OrderStatus Completed => OrderStatusService.Completed;
    }
}
