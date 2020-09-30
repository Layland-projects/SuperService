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
        IngredientService _serv = new IngredientService(new SuperServiceContext());

        public IEnumerable<Ingredient> GetAllIngredientsWithDistinctNames()
        {
            return _serv.GetAllIngredients().Distinct(new IngredientSameNameComparer()).OrderBy(x => x.Name);
        }

        public Ingredient GetIngredientByID(int id) => _serv.GetIngredientByID(id);
        public IEnumerable<Ingredient> GetIngredientsByName(string name) => _serv.GetIngredientsByName(name);

        public void AddNewIngredient(Ingredient ingredient)
        {
            _serv.AddNewIngredient(ingredient);
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            _serv.RemoveIngredient(ingredient);
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            _serv.UpdateIngredient(ingredient);
        }

        public Ingredient UndoIngredientChanges(Ingredient ingredient)
        {
            _serv.RecycleConnection();
            return _serv.GetIngredientByID(ingredient.IngredientID);
        }
    }
}
