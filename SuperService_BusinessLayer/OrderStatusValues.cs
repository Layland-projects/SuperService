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

        public static bool IsValidStatus(OrderStatus status) => status.OrderStatusID == OrderPlaced.OrderStatusID || status.OrderStatusID == InProcess.OrderStatusID || status.OrderStatusID == ReadyToCollect.OrderStatusID || status.OrderStatusID == Completed.OrderStatusID;
    }
}
