using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public virtual Table Table { get; set; }
        public int OrderStatusID { get; set; }
        public  OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderItems> Items { get; set; }
        public string DisplayOrderID => $"Order number: {OrderID}";
    }
}
