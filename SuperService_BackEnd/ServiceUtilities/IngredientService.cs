using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperService_BackEnd.ServiceUtilities
{
    public class IngredientService
    {
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            using (var db = new SuperServiceContext())
            {
                return db.Ingredients.ToList();
            }
        }
        public Ingredient GetIngredientByID(int id) => GetAllIngredients().Where(x => x.IngredientID == id).FirstOrDefault(); 

        public IEnumerable<Ingredient> GetIngredientsByName(string name) => GetAllIngredients().Where(x => x.Name == name);

        public void AddNewIngredient(Ingredient ingredient)
        {
            using (var db = new SuperServiceContext())
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
            }
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            using (var db = new SuperServiceContext())
            {
                var dbIngredient = db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
                if (dbIngredient != null)
                {
                    db.Ingredients.Remove(dbIngredient);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            using (var db = new SuperServiceContext())
            {
                var ingredientInDb = db.Ingredients.Where(x => x.IngredientID == ingredient.IngredientID).FirstOrDefault();
                foreach (var prop in ingredient.GetType().GetProperties())
                {
                    if (prop.Name != "IngredientID" && prop.CanWrite)
                    {
                        prop.SetValue(ingredientInDb, prop.GetValue(ingredient));
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
