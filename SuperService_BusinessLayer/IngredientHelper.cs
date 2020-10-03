using Microsoft.EntityFrameworkCore.Internal;
using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using SuperService_BackEnd.ServiceUtilities;
using SuperService_BusinessLayer.Comparers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SuperService_BusinessLayer
{
    public class IngredientHelper
    {
        IngredientService _serv = new IngredientService();

        public IEnumerable<Ingredient> GetAllIngredientsWithDistinctNames()
        {
            return _serv.GetAllIngredients().Distinct(new IngredientSameNameComparer()).OrderBy(x => x.Name);
        }

        public Ingredient GetIngredientByID(int id) => _serv.GetIngredientByID(id);
        public IEnumerable<Ingredient> GetIngredientsByName(string name) => _serv.GetIngredientsByName(name);

        //AddNewIngredient is currently not being used in the project, just to test
        public void AddNewIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                _serv.AddNewIngredient(ingredient);
            }
        }

        //RemoveIngredient is currently not being used in the project, just to test
        public void RemoveIngredient(Ingredient ingredient)
        {
            _serv.RemoveIngredient(ingredient);
        }

        public void DecrementStock(Ingredient ingredient)
        {
            ingredient.NumberInStock--;
            UpdateIngredient(ingredient);
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                _serv.UpdateIngredient(ingredient);
            }
        }

        public Ingredient UndoIngredientChanges(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                return _serv.GetIngredientByID(ingredient.IngredientID);
            }
            return new Ingredient();
        }
    }
}
