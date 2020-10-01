using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class ItemService
    {
        public IEnumerable<Item> GetAllItems()
        {
            using (var db = new SuperServiceContext())
            {
                return db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).ToList();
            }
        }

        public Item GetItemByID(int id)
        {
            return GetAllItems().Where(x => x.ItemID == id).FirstOrDefault();
        }

        public void AddNewItem(Item item)
        {
            using (var db = new SuperServiceContext())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }

        public void RemoveItem(Item item)
        {
            using (var db = new SuperServiceContext())
            {
                var itemInDb = db.Items.Where(x => x.ItemID == item.ItemID);
                db.Items.RemoveRange(itemInDb);
                db.SaveChanges();
            }
        }
    }
}
