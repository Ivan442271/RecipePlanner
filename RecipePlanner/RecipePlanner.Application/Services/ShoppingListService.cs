using System;
using System.Collections.Generic;
using System.Text;
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
                    Ingredient existingIngredient = shoppingList
                        .FirstOrDefault(i => i.Name == ingredient.Name);

                    if (existingIngredient != null)
                    {
                        existingIngredient.Amount += ingredient.Amount;
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

            return shoppingList;
        }
    }
}