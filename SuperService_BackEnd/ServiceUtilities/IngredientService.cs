using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SuperService_BackEnd.Interfaces;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class IngredientService : IIngredientService
    {
        SuperServiceContext _db = new SuperServiceContext();

        public IngredientService() { }
        public IngredientService(SuperServiceContext db)
        {
            _db = db;
        }
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return _db.Ingredients.AsNoTracking().ToList();
        }
        public Ingredient GetIngredientByID(int id) => GetAllIngredients().Where(x => x.IngredientID == id).FirstOrDefault();

        public IEnumerable<Ingredient> GetIngredientsByName(string name) => GetAllIngredients().Where(x => x.Name == name);

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

        public void DecrementStockForIngredient(Ingredient ingredient)
        {
            var ingInDb = _db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
            ingInDb.NumberInStock--;
            _db.SaveChanges();
        }

        public void IncrementStockForIngredient(Ingredient ingredient)
        {

            var ingInDb = _db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
            ingInDb.NumberInStock++;
            _db.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            var ingredientInDb = _db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
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
