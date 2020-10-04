using NUnit.Framework;
using SuperService_BackEnd.Models;
using SuperService_BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperService_BusinessLayer.Tests
{
    [TestFixture]
    public class ItemHelperTests
    {
        ItemHelper iHelper = new ItemHelper();
        IngredientHelper ingHelper = new IngredientHelper();
        [SetUp]
        public void SetUp()
        {
            var testItems = iHelper.GetAllItemsOrderedByAvailability().Where(x => x.Name.Contains("Test"));
            var testIngredients = ingHelper.GetAllIngredientsWithDistinctNames().Where(x => x.Name.Contains("Test"));
            foreach(var item in testItems)
            {
                iHelper.RemoveItem(item);
            }
            foreach(var item in testIngredients)
            {
                ingHelper.RemoveIngredient(item);
            }
            iHelper.AddNewItem(new Item { Cost = 5, Name = "TestSandwich" }, new List<Ingredient>
            {
                new Ingredient { Name = "TestBread", Calories = 230, Carbohydrates = 15, Fat = 1, Protein = 1, Salt = 5, Sugar = 5, NumberInStock = 10 },
                new Ingredient { Name = "TestCheese", Calories = 150, Carbohydrates = 1, Fat = 10, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 10 }
            });
            iHelper.AddNewItem(new Item { Cost = 8, Name = "TestStew" }, new List<Ingredient>
            {
                new Ingredient { Name = "TestCabbage", Calories = 60, Carbohydrates = 15, Fat = 1, Protein = 1, Salt = 5, Sugar = 5, NumberInStock = 10 },
                new Ingredient { Name = "TestPotatoes", Calories = 300, Carbohydrates = 60, Fat = 3, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 10 },
                new Ingredient { Name = "TestCarrots", Calories = 100, Carbohydrates = 20, Fat = 10, Protein = 2, Salt = 5, Sugar = 3, NumberInStock = 0 },
            });
        }
        [TearDown]
        public void TearDown()
        {
            var testItems = iHelper.GetAllItemsOrderedByAvailability().Where(x => x.Name.Contains("Test"));
            var testIngredients = ingHelper.GetAllIngredientsWithDistinctNames().Where(x => x.Name.Contains("Test"));
            foreach (var item in testItems)
            {
                iHelper.RemoveItem(item);
            }
            foreach (var item in testIngredients)
            {
                ingHelper.RemoveIngredient(item);
            }
        }

        [Test]
        public void GetAllItemsOrderedByAvailabilityTest()
        {
            var items = iHelper.GetAllItemsOrderedByAvailability().ToList();
            Assert.IsTrue(items.First().CanOrder);
            Assert.IsFalse(items.Last().CanOrder);
        }

        [Test]
        public void GetItemByIDTest()
        {
            var testItems = iHelper.GetAllItemsOrderedByAvailability().Where(x => x.Name.StartsWith("Test"));
            foreach (var item in testItems)
            {
                Assert.AreEqual(item.Cost, iHelper.GetItemByID(item.ItemID).Cost);
            }
        }
        [Test]
        public void GetItemByIDTest_WithInvalidID()
        {
            Assert.Null(iHelper.GetItemByID(int.MaxValue));
        }


        [Test]
        public void DecrementStockForItem_DecrementsIngredientStockCountTest()
        {
            var testItems = iHelper.GetAllItemsOrderedByAvailability().Where(x => x.Name.StartsWith("Test")).ToList();
            var allIngredients = ingHelper.GetAllIngredientsWithDistinctNames().ToList();
            var ingredientDict = new Dictionary<string, int>();
            foreach (var item in testItems)
            {
                foreach (var itemIngredient in item.ItemIngredients)
                {
                    if (ingredientDict.ContainsKey(itemIngredient.Ingredient.Name))
                    {
                        ingredientDict[itemIngredient.Ingredient.Name]++;
                    }
                    else
                    {
                        ingredientDict.Add(itemIngredient.Ingredient.Name, 1);
                    }
                }
            }
            foreach (var ingredient in allIngredients)
            {
                if (ingredientDict.ContainsKey(ingredient.Name))
                {
                    ingredientDict[ingredient.Name] = (ingredient.NumberInStock - ingredientDict[ingredient.Name] < 0) ? 0 : ingredient.NumberInStock - ingredientDict[ingredient.Name];
                }
            }
            foreach (var item in testItems)
            {
                iHelper.DecremenetStockForItem(item);
            }
            foreach (var entry in ingredientDict)
            {
                Assert.AreEqual(entry.Value, ingHelper.GetIngredientsByName(entry.Key).First().NumberInStock);
            }
        }

        [Test]
        public void IncrementStockForItem_IncrementsIngredientStockCountTest()
        {
            var testItems = iHelper.GetAllItemsOrderedByAvailability().Where(x => x.Name.StartsWith("Test")).ToList();
            var allIngredients = ingHelper.GetAllIngredientsWithDistinctNames().ToList();
            var ingredientDict = new Dictionary<string, int>();
            foreach (var item in testItems)
            {
                foreach (var itemIngredient in item.ItemIngredients)
                {
                    if (ingredientDict.ContainsKey(itemIngredient.Ingredient.Name))
                    {
                        ingredientDict[itemIngredient.Ingredient.Name]++;
                    }
                    else
                    {
                        ingredientDict.Add(itemIngredient.Ingredient.Name, 1);
                    }
                }
            }
            foreach (var ingredient in allIngredients)
            {
                if (ingredientDict.ContainsKey(ingredient.Name))
                {
                    ingredientDict[ingredient.Name] = (ingredient.NumberInStock + ingredientDict[ingredient.Name] < 0) ? 0 : ingredient.NumberInStock + ingredientDict[ingredient.Name];
                }
            }
            foreach (var item in testItems)
            {
                iHelper.IncrementStockForItem(item);
            }
            foreach (var entry in ingredientDict)
            {
                Assert.AreEqual(entry.Value, ingHelper.GetIngredientsByName(entry.Key).First().NumberInStock);
            }
        }
    }
}