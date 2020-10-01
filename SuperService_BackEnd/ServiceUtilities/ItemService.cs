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
    }
}
