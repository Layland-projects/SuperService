using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class UserType
    {
        public int UserTypeID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
