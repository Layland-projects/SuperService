using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SuperService_BackEnd.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }
        [Required]
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Sugar { get; set; }
        public double Carbohydrates { get; set; }
        public double Salt { get; set; }
        int _numberInStock;
        public int NumberInStock 
        {
            get => _numberInStock;
            set => _numberInStock = value >= 0 ? value : 0;
        }
        public bool IsInStock => NumberInStock > 0;
        public ICollection<ItemIngredients> ItemIngredients { get; set; }
    }
}
