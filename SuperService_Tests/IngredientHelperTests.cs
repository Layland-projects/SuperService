using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer.Tests
{
    [TestFixture()]
    public class IngredientHelperTests
    {
        Ingredient ingredient;
        Ingredient ingredient2;
        IngredientHelper _iHelper = new IngredientHelper();
        [SetUp]
        public void SetUp()
        {
            var testIngredients = _iHelper.GetIngredientsByName("TestCabbage").ToList();
            ingredient = testIngredients.Count > 0 ? testIngredients.First() : new Ingredient() { Name = "TestCabbage", Calories = 1, Carbohydrates = 1, Fat = 1, NumberInStock = 1, Protein = 1, Salt = 1, Sugar = 1 };
            ingredient2 = testIngredients.Count > 0 ? testIngredients.Last() : new Ingredient() { Name = "TestCabbage", Calories = 1, Carbohydrates = 1, Fat = 1, NumberInStock = 1, Protein = 1, Salt = 1, Sugar = 1 };
            _iHelper.RemoveIngredient(ingredient);
            _iHelper.RemoveIngredient(ingredient2);
            if (testIngredients.Count > 0)
            {
                ingredient.IngredientID = 0;
                ingredient2.IngredientID = 0;
            }
            _iHelper.AddNewIngredient(ingredient);
            _iHelper.AddNewIngredient(ingredient2);
            ingredient = _iHelper.GetIngredientsByName(ingredient.Name).OrderBy(x => x.IngredientID).First();
            ingredient2 = _iHelper.GetIngredientsByName(ingredient.Name).OrderBy(x => x.IngredientID).Last();
        }
        [TearDown]
        public void TearDown()
        {
            _iHelper.RemoveIngredient(ingredient);
            _iHelper.RemoveIngredient(ingredient2);
        }

        [TestCase(55, 55)]
        [TestCase(0, 0)]
        [TestCase(-5, 0)]
        [TestCase(200000, 200000)]
        public void UpdateIngredientStockTest(int newValue, int expected)
        {
            ingredient.NumberInStock = newValue;
            _iHelper.UpdateIngredient(ingredient);
            Assert.AreEqual(expected, _iHelper.GetIngredientByID(ingredient.IngredientID).NumberInStock);
        }

        [Test]
        public void GetAllIngredientsWithDistinctNamesTest()
        {
            Assert.IsTrue(_iHelper.GetAllIngredientsWithDistinctNames().Where(x => x.Name == ingredient.Name).Count() == 1);
        }

        [Test]
        public void UndoIngredientStockChangesTest()
        {
            ingredient.NumberInStock = 5;
            ingredient = _iHelper.UndoIngredientChanges(ingredient);
            Assert.AreEqual(1, ingredient.NumberInStock);
        }
        [Test]
        public void GetIngredientsByNameTest()
        {
            Assert.IsTrue(_iHelper.GetIngredientsByName("TestCabbage").Count() > 0);
        }
    }
}