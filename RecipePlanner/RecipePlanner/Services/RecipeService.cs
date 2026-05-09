using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Core.Interfaces;
using RecipePlanner.Core.Models;
using RecipePlanner.Core.Enums;

namespace RecipePlanner.Services;

public class RecipeService
{
    private readonly IRepository<Recipe> _repository;

    public RecipeService(IRepository<Recipe> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Recipe> GetRecipesByCategory(Category category)
    {
        return _repository.GetAll().Where(r => r.Category == category);
    }

    public IEnumerable<Recipe> SearchByName(string name)
    {
        return _repository.GetAll().Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
    public Recipe ScaleRecipe(Recipe recipe, int currentPortions, int targetPortions)
    {
        double factor = (double)targetPortions / currentPortions;

        foreach (var ingredient in recipe.Ingredients)
        {
            ingredient.Amount *= factor;
        }

        return recipe;
    }
    public void AddRecipe(Recipe recipe)
    {
        if (string.IsNullOrWhiteSpace(recipe.Name))
        {
            throw new ArgumentException("Назва рецепту не може бути порожньою");
        }
        _repository.Add(recipe);
        _repository.Save();
    }

    public void UpdateRecipe(Recipe recipe)
    {
        _repository.Update(recipe);
        _repository.Save();
    }

    public void DeleteRecipe(Guid id)
    {
        _repository.Delete(id);
        _repository.Save();
    }

    public Recipe GetRecipeById(Guid id)
    {
        return _repository.GetById(id);
    }
}
