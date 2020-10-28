using SuperService_BackEnd.Models;
using System.Collections.Generic;

namespace SuperService_BackEnd.Interfaces
{
    public interface IIngredientService
    {
        void AddNewIngredient(Ingredient ingredient);
        void DecrementStockForIngredient(Ingredient ingredient);
        IEnumerable<Ingredient> GetAllIngredients();
        Ingredient GetIngredientByID(int id);
        IEnumerable<Ingredient> GetIngredientsByName(string name);
        void IncrementStockForIngredient(Ingredient ingredient);
        void RemoveIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
    }
}