using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class ItemHelper
    {
        ItemService _serv = new ItemService();
        IngredientService _ingServ = new IngredientService();
        ItemIngredientService _iIServ = new ItemIngredientService();

        public ItemHelper() { }
        public ItemHelper(SuperServiceContext db)
        {
            _serv = new ItemService(db);
            _ingServ = new IngredientService(db);
            _iIServ = new ItemIngredientService(db);
        }
        public IEnumerable<Item> GetAllItemsOrderedByAvailability() => _serv.GetAllItems().OrderByDescending(x => x.CanOrder).ThenBy(x => x.Name);
        public Item GetItemByID(int id) => _serv.GetItemByID(id);

        //currently AddNewItem only exists to allow me to test, if it didn't work most of the tests wouldn't pass
        public void AddNewItem(Item item, IEnumerable<Ingredient> ingredients)
        {
            if (item != null && ingredients.Count() > 0)
            {
                _serv.AddNewItem(item);
                var ingredientsWithID = new List<Ingredient>();
                foreach (var ingredient in ingredients)
                {
                    if (_ingServ.GetIngredientsByName(ingredient.Name).Count() == 0)
                    {
                        _ingServ.AddNewIngredient(ingredient);
                    }
                    ingredientsWithID.AddRange(_ingServ.GetIngredientsByName(ingredient.Name));
                }
                var itemIngredients = new List<ItemIngredients>();
                foreach(var ingredient in ingredientsWithID)
                {
                    itemIngredients.Add(new ItemIngredients { IngredientID = ingredient.IngredientID, ItemID = item.ItemID });
                }
                _iIServ.AddItemIngredients(itemIngredients);
            }
        }

        //currently RemoveItem only exists to allow me to test, if it didn't work most of the tests wouldn't pass
        public void RemoveItem(Item item)
        {
            _iIServ.RemoveItemIngredientsByItemID(item.ItemID);
            _serv.RemoveItem(item);
        }

        public void DecremenetStockForItem(Item item)
        {
            foreach (var itemIng in item.ItemIngredients)
            {
                _ingServ.DecrementStockForIngredient(itemIng.Ingredient);
            }
        }

        public void IncrementStockForItem(Item item)
        {
            foreach (var itemIng in item.ItemIngredients)
            {
                _ingServ.IncrementStockForIngredient(itemIng.Ingredient);
            }
        }
    }
}
