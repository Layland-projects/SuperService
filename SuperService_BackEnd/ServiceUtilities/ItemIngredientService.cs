using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class ItemIngredientService
    {
        public void AddItemIngredient(ItemIngredients itemIngredients)
        {
            using (var db = new SuperServiceContext())
            {
                db.ItemIngredients.Add(itemIngredients);
                db.SaveChanges();
            }
        }
        public void AddItemIngredients(IEnumerable<ItemIngredients> itemIngredients)
        {
            foreach (var item in itemIngredients)
            {
                AddItemIngredient(item);
            }
        }
        public void RemoveItemIngredientsByItemID(int id)
        {
            using (var db = new SuperServiceContext())
            {
                db.ItemIngredients.RemoveRange(db.ItemIngredients.Where(x => x.ItemID == id));
                db.SaveChanges();
            }
        }
    }
}
