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
        SuperServiceContext _db = new SuperServiceContext();
        public ItemService() { }
        public ItemService(SuperServiceContext db)
        {
            _db = db;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).AsNoTracking().ToList();
        }

        public Item GetItemByID(int id)
        {
            return GetAllItems().Where(x => x.ItemID == id).FirstOrDefault();
        }

        public void AddNewItem(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }

        public void RemoveItem(Item item)
        {
            var itemInDb = _db.Items.Where(x => x.ItemID == item.ItemID);
            _db.Items.RemoveRange(itemInDb);
            _db.SaveChanges();
        }
    }
}
