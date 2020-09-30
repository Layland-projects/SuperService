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
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            return _db.Ingredients;
        }
        public Ingredient GetIngredientByID(int id) => _db.Ingredients.Where(x => x.IngredientID == id).FirstOrDefault();

    }
}
