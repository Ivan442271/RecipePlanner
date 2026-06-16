using System;
using System.Collections.Generic;
using System.Linq;
using RecipePlanner.Domain.Enums;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.Application.Services
{
    public class ShoppingListService
    {
        public List<Ingredient> CreateShoppingList(List<Recipe> recipes)
        {
            List<Ingredient> shoppingList = new List<Ingredient>();

            foreach (Recipe recipe in recipes)
            {
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    Ingredient existing = shoppingList
                        .FirstOrDefault(i => i.Name.ToLower() == ingredient.Name.ToLower() && i.UnitType == ingredient.UnitType);

                    if (existing != null)
                    {
                        existing.Amount += ingredient.Amount;
                    }
                    else
                    {
                        shoppingList.Add(new Ingredient
                        {
                            Name = ingredient.Name,
                            Amount = ingredient.Amount,
                            UnitType = ingredient.UnitType
                        });
                    }
                }
            }

            return shoppingList
                .OrderBy(i => i.Name.ToLower())
                .ThenBy(i => GetUnitSortOrder(i.UnitType))
                .ToList();
        }

        private int GetUnitSortOrder(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Kilograms: return 1;
                case UnitType.Grams: return 2;
                case UnitType.Liters: return 3;
                case UnitType.Milliliters: return 4;
                default: return 5;
            }
        }
    }
}