using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer.ViewModels
{
    public class OrderItemViewModel
    {
        public int TableNumber { get; set; }
        public int OrderNumber { get; set; }
        public Item Item { get; set; }
    }
}
