using Microsoft.EntityFrameworkCore;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class ItemService
    {
        SuperServiceContext _db;
        public ItemService(SuperServiceContext db)
        {
            _db = db;
        }

        public IEnumerable<Item> GetAllItems() => _db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient);

    }
}
