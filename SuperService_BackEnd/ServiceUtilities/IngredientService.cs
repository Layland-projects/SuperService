using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class IngredientService
    {
        SuperServiceContext _db;
        public IngredientService(SuperServiceContext db)
        {
            _db = db;
        }
        public IEnumerable<Ingredient> GetAllIngredients() => _db.Ingredients;
        public Ingredient GetIngredientByID(int id) => _db.Ingredients.Where(x => x.IngredientID == id).FirstOrDefault();
        public IEnumerable<Ingredient> GetIngredientsByName(string name) => _db.Ingredients.Where(x => x.Name == name);
        public void RecycleConnection() => _db = new SuperServiceContext();

        public void AddNewIngredient(Ingredient ingredient)
        {
            _db.Ingredients.Add(ingredient);
            _db.SaveChanges();
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            var dbIngredient = _db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
            if (dbIngredient != null)
            {
                _db.Ingredients.Remove(dbIngredient);
                _db.SaveChanges();
            }
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            var ingredientInDb = GetIngredientByID(ingredient.IngredientID);
            foreach (var prop in ingredient.GetType().GetProperties())
            {
                if (prop.Name != "IngredientID" && prop.CanWrite)
                {
                    prop.SetValue(ingredientInDb, prop.GetValue(ingredient));
                }
            }
            _db.SaveChanges();
        }
    }
}
