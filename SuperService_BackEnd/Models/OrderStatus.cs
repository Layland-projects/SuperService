using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class OrderStatus
    {
        public int OrderStatusID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
