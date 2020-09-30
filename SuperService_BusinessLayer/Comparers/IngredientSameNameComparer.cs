using SuperService_BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SuperService_BusinessLayer.Comparers
{
    public class IngredientSameNameComparer : IEqualityComparer<Ingredient>
    {
        public bool Equals([AllowNull] Ingredient x, [AllowNull] Ingredient y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Ingredient obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
