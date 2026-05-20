using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RecipePlanner.Application.Services;
using RecipePlanner.Domain.Enums;
using RecipePlanner.Domain.Models;
using RecipePlanner.Tests.Fakes;

namespace RecipePlanner.Tests.Services
{
    public class RecipeServiceTests
    {
        private RecipeService _recipeService;

        [SetUp]
        public void Setup()
        {
            FakeRecipeRepository repository =
                new FakeRecipeRepository();

            _recipeService = new RecipeService(repository);
        }

        [Test]
        public void AddRecipe_ShouldAddRecipe()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.GetAllRecipes();

            Assert.That(recipes.Count, Is.EqualTo(1));
        }

        [Test]
        public void DeleteRecipe_ShouldRemoveRecipe()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";

            _recipeService.AddRecipe(recipe);

            _recipeService.DeleteRecipe(0);

            List<Recipe> recipes =
                _recipeService.GetAllRecipes();

            Assert.That(recipes.Count, Is.EqualTo(0));
        }

        [Test]
        public void DeleteRecipe_InvalidIndex_ShouldThrowException()
        {
            Assert.Throws<Exception>(() =>
            {
                _recipeService.DeleteRecipe(10);
            });
        }

        [Test]
        public void SearchByName_ShouldFindRecipe()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.SearchByName("Pizza");

            Assert.That(recipes.Count, Is.EqualTo(1));
        }

        [Test]
        public void SearchByName_ShouldReturnEmptyList()
        {
            List<Recipe> recipes =
                _recipeService.SearchByName("Burger");

            Assert.That(recipes.Count, Is.EqualTo(0));
        }

        [Test]
        public void FilterByCategory_ShouldReturnFilteredRecipes()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Cake";
            recipe.Category = RecipeCategory.Dessert;

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.FilterByCategory(
                    RecipeCategory.Dessert);

            Assert.That(recipes.Count, Is.EqualTo(1));
        }

        [Test]
        public void SortRecipes_ByName_ShouldSortCorrectly()
        {
            Recipe recipe1 = new Recipe();
            recipe1.Name = "Pizza";

            Recipe recipe2 = new Recipe();
            recipe2.Name = "Burger";

            _recipeService.AddRecipe(recipe1);
            _recipeService.AddRecipe(recipe2);

            List<Recipe> recipes =
                _recipeService.SortRecipes(
                    SortOption.ByName);

            Assert.That(recipes[0].Name,
                Is.EqualTo("Burger"));
        }

        [Test]
        public void UpdateRecipe_ShouldUpdateRecipe()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";

            _recipeService.AddRecipe(recipe);

            recipe.Notes = "Updated";

            _recipeService.UpdateRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.GetAllRecipes();

            Assert.That(recipes[0].Notes,
                Is.EqualTo("Updated"));
        }

        [Test]
        public void SearchByName_ShouldIgnoreCase()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.SearchByName("pizza");

            Assert.That(recipes.Count, Is.EqualTo(1));
        }

        [Test]
        public void FilterByCategory_ShouldReturnEmptyList()
        {
            List<Recipe> recipes =
                _recipeService.FilterByCategory(
                    RecipeCategory.Dessert);

            Assert.That(recipes.Count, Is.EqualTo(0));
        }

        [Test]
        public void SortRecipes_ByCookingTime_ShouldSortCorrectly()
        {
            Recipe recipe1 = new Recipe();
            recipe1.Name = "Pizza";
            recipe1.CookingTimeInMinutes = 30;

            Recipe recipe2 = new Recipe();
            recipe2.Name = "Soup";
            recipe2.CookingTimeInMinutes = 10;

            _recipeService.AddRecipe(recipe1);
            _recipeService.AddRecipe(recipe2);

            List<Recipe> recipes =
                _recipeService.SortRecipes(
                    SortOption.ByCookingTime);

            Assert.That(recipes[0].Name,
                Is.EqualTo("Soup"));
        }

        [Test]
        public void SortRecipes_ByIngredientCount_ShouldSortCorrectly()
        {
            Recipe recipe1 = new Recipe();
            recipe1.Name = "Pizza";
            recipe1.Ingredients =
                new List<Ingredient>
                {
            new Ingredient(),
            new Ingredient()
                };

            Recipe recipe2 = new Recipe();
            recipe2.Name = "Soup";
            recipe2.Ingredients =
                new List<Ingredient>
                {
            new Ingredient()
                };

            _recipeService.AddRecipe(recipe1);
            _recipeService.AddRecipe(recipe2);

            List<Recipe> recipes =
                _recipeService.SortRecipes(
                    SortOption.ByIngredientCount);

            Assert.That(recipes[0].Name,
                Is.EqualTo("Soup"));
        }

        [Test]
        public void AddRecipe_ShouldSaveFavoriteRecipe()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";
            recipe.IsFavorite = true;

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.GetAllRecipes();

            Assert.That(recipes[0].IsFavorite,
                Is.True);
        }

        [Test]
        public void AddRecipe_ShouldSaveNotes()
        {
            Recipe recipe = new Recipe();

            recipe.Name = "Pizza";
            recipe.Notes = "Test notes";

            _recipeService.AddRecipe(recipe);

            List<Recipe> recipes =
                _recipeService.GetAllRecipes();

            Assert.That(recipes[0].Notes,
                Is.EqualTo("Test notes"));
        }
    }
}
