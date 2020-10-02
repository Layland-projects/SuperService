using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SuperService_BackEnd;
using SuperService_BackEnd.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SuperService_Tests
{
    public class ContextTests
    {
        [SetUp]
        public void Setup()
        {
            using (var db = new SuperServiceContext())
            {
                var burger = new Item() { Cost = 5.50m, Name = "TestCheeseburger" };
                var bun = new Ingredient() { Name = "TestSesame seed bun", Calories = 220, Carbohydrates = 40, Fat = 0, Protein = 0, Salt = 4, Sugar = 10, NumberInStock = 120 };
                var cheese = new Ingredient() { Name = "TestCheese slice", Calories = 120, Carbohydrates = 3, Fat = 15, Protein = 1, Salt = 2, Sugar = 3, NumberInStock = 50 };
                var beefBurger = new Ingredient() { Name = "TestQuarter pound beef burger", Calories = 250, Carbohydrates = 6, Fat = 15, Protein = 28, Salt = 10, Sugar = 1, NumberInStock = 80 };

                var ingredientList = new List<Ingredient>()
                {
                    bun,
                    cheese,
                    beefBurger
                };

                foreach (var ingredient in ingredientList)
                {
                    if (db.Ingredients.Where(x => x.Name == ingredient.Name).Count() > 0)
                    {
                        var ingId = db.Ingredients.Where(x => x.Name == ingredient.Name).First().IngredientID;
                        db.ItemIngredients.RemoveRange(db.ItemIngredients.Include(x => x.Ingredient).Where(x => x.Ingredient.IngredientID == ingId));
                        db.SaveChanges();
                        db.Ingredients.Remove(db.Ingredients.Where(x => x.Name == ingredient.Name).FirstOrDefault());
                        db.SaveChanges();
                    }
                }

                if (db.Items.Where(x => x.Name == burger.Name).Count() > 0)
                {
                    db.Items.RemoveRange(db.Items.Where(x => x.Name == burger.Name));
                }

                db.ItemIngredients.Add(new ItemIngredients() { Ingredient = bun, Item = burger });
                db.ItemIngredients.Add(new ItemIngredients() { Ingredient = cheese, Item = burger });
                db.ItemIngredients.Add(new ItemIngredients() { Ingredient = beefBurger, Item = burger });
                db.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new SuperServiceContext())
            {
                var burger = new Item() { Cost = 5.50m, Name = "TestCheeseburger" };
                var bun = new Ingredient() { Name = "TestSesame seed bun", Calories = 220, Carbohydrates = 40, Fat = 0, Protein = 0, Salt = 4, Sugar = 10, NumberInStock = 120 };
                var cheese = new Ingredient() { Name = "TestCheese slice", Calories = 120, Carbohydrates = 3, Fat = 15, Protein = 1, Salt = 2, Sugar = 3, NumberInStock = 50 };
                var beefBurger = new Ingredient() { Name = "TestQuarter pound beef burger", Calories = 250, Carbohydrates = 6, Fat = 15, Protein = 28, Salt = 10, Sugar = 1, NumberInStock = 80 };

                var ingredientList = new List<Ingredient>()
                    {
                        bun,
                        cheese,
                        beefBurger
                    };

                foreach (var ingredient in ingredientList)
                {
                    if (db.Ingredients.Where(x => x.Name == ingredient.Name).Count() > 0)
                    {
                        var ingId = db.Ingredients.Where(x => x.Name == ingredient.Name).First().IngredientID;
                        db.ItemIngredients.RemoveRange(db.ItemIngredients.Include(x => x.Ingredient).Where(x => x.Ingredient.IngredientID == ingId));
                        db.SaveChanges();
                        db.Ingredients.Remove(db.Ingredients.Where(x => x.IngredientID == ingId).FirstOrDefault());
                        db.SaveChanges();
                    }
                }

                if (db.Items.Where(x => x.Name == burger.Name).Count() > 0)
                {
                    db.Items.RemoveRange(db.Items.Where(x => x.Name == burger.Name));
                }
                db.SaveChanges();
            }
        }

        [Test]
        public void WhenIQueryItems_ICanAccessAssociatedIngredients()
        {
            using (var db = new SuperServiceContext())
            {
                var itemCount = db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Include(x => x.ItemIngredients).ThenInclude(x => x.Item).Count();
                var items = db.Items.First();
                Assert.IsTrue(itemCount > 0);
                var ingredients = db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).Select(x => x.ItemIngredients.Select(x => x.Ingredient));
                Assert.IsTrue(ingredients.Count() > 0);
            }
        }

        [Test]
        public void WhenIIncludeItemIngredientsAndIngredients_MyTotalCalculatedPropertiesAreCalculated()
        {
            using (var db = new SuperServiceContext())
            {
                var i = db.Items.Include(x => x.ItemIngredients).ThenInclude(x => x.Ingredient).FirstOrDefault();
                Assert.IsTrue(i.Calories > 0);
                Assert.IsTrue(i.Fat > 0);
                Assert.IsTrue(i.Carbohydrates > 0);
                Assert.IsTrue(i.Protein > 0);
                Assert.IsTrue(i.Salt > 0);
                Assert.IsTrue(i.Sugar > 0);
            }
        }
    }
}