using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class ItemIngredients
    {
        [Key]
        public int ItemIngredientID { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
