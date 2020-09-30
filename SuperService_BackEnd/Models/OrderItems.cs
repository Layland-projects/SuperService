using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class OrderItems
    {
        public int OrderItemsID { get; set; }
        //public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        //public int ItemID { get; set; }
        public virtual Item Item { get; set; }

    }
}
