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
        SuperServiceContext _db = new SuperServiceContext();

        public ItemIngredientService() { }
        public ItemIngredientService(SuperServiceContext db)
        {
            _db = db;
        }

        public void AddItemIngredient(ItemIngredients itemIngredients)
        {
            _db.ItemIngredients.Add(itemIngredients);
            _db.SaveChanges();
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
            _db.ItemIngredients.RemoveRange(_db.ItemIngredients.Where(x => x.ItemID == id));
            _db.SaveChanges();
        }
    }
}
