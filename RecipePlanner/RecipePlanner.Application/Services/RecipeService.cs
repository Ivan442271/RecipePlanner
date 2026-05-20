using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Application.Repositories;
using RecipePlanner.Domain.Enums;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.Application.Services
{
    public class RecipeService
    {
        private readonly JsonRecipeRepository _repository;

        public RecipeService()
        {
            _repository = new JsonRecipeRepository();
        }

        public List<Recipe> GetAllRecipes()
        {
            return _repository.GetAll();
        }

        public void AddRecipe(Recipe recipe)
        {
            List<Recipe> recipes = _repository.GetAll();

            recipes.Add(recipe);

            _repository.SaveAll(recipes);
        }

        public void DeleteRecipe(int index)
        {
            List<Recipe> recipes = _repository.GetAll();

            if (index < 0 || index >= recipes.Count)
            {
                throw new Exception("Invalid recipe index.");
            }

            recipes.RemoveAt(index);

            _repository.SaveAll(recipes);
        }

        public List<Recipe> SearchByName(string name)
        {
            List<Recipe> recipes = _repository.GetAll();

            List<Recipe> foundRecipes = new List<Recipe>();

            foreach (Recipe recipe in recipes)
            {
                if (recipe.Name.ToLower().Contains(name.ToLower()))
                {
                    foundRecipes.Add(recipe);
                }
            }

            return foundRecipes;
        }

        public List<Recipe> FilterByCategory(RecipeCategory category)
        {
            List<Recipe> recipes = _repository.GetAll();

            List<Recipe> filteredRecipes = new List<Recipe>();

            foreach (Recipe recipe in recipes)
            {
                if (recipe.Category == category)
                {
                    filteredRecipes.Add(recipe);
                }
            }

            return filteredRecipes;
        }

        public List<Recipe> SortRecipes(SortOption sortOption)
        {
            List<Recipe> recipes = _repository.GetAll();

            if (sortOption == SortOption.ByName)
            {
                recipes = recipes.OrderBy(recipe => recipe.Name).ToList();
            }

            if (sortOption == SortOption.ByCookingTime)
            {
                recipes = recipes.OrderBy(recipe => recipe.CookingTimeInMinutes).ToList();
            }

            if (sortOption == SortOption.ByIngredientCount)
            {
                recipes = recipes.OrderBy(recipe => recipe.Ingredients.Count).ToList();
            }

            return recipes;
        }

        public void UpdateRecipe(Recipe updatedRecipe)
        {
            List<Recipe> recipes = _repository.GetAll();

            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].Name == updatedRecipe.Name)
                {
                    recipes[i] = updatedRecipe;
                    break;
                }
            }

            _repository.SaveAll(recipes);
        }
    }
}
