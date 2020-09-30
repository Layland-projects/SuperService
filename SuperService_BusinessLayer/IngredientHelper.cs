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
    }
}
