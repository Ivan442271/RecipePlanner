using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Application.Services;
using RecipePlanner.Domain.Enums;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.ConsoleUI.Menus
{
    internal class MainMenu
    {
        private readonly RecipeService _recipeService;

        public MainMenu()
        {
            _recipeService = new RecipeService();
        }

        public void Show()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("=== Recipe Planner ===");
                Console.WriteLine("1. Recipes");
                Console.WriteLine("2. Add recipe");
                Console.WriteLine("3. Favorites");
                Console.WriteLine("4. Settings");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowRecipesMenu();
                        break;

                    case "2":
                        AddRecipe();
                        break;

                    case "3":
                        ShowFavorites();
                        break;

                    case "4":
                        ShowSettings();
                        break;

                    case "0":
                        isRunning = false;
                        break;
                }
            }
        }

        private void ShowRecipesMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine("1. Show all");
                Console.WriteLine("2. Search");
                Console.WriteLine("3. Filter");
                Console.WriteLine("4. Sort");
                Console.WriteLine("0. Back");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowRecipes(_recipeService.GetAllRecipes());
                        break;

                    case "2":
                        SearchRecipes();
                        break;

                    case "3":
                        FilterRecipes();
                        break;

                    case "4":
                        SortRecipes();
                        break;

                    case "0":
                        isRunning = false;
                        break;
                }
            }
        }

        private void ShowRecipes(List<Recipe> recipes)
        {
            Console.Clear();

            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < recipes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipes[i].Name}");
            }

            Console.WriteLine();
            Console.Write("Select recipe number: ");

            int index = int.Parse(Console.ReadLine()) - 1;

            if (index < 0 || index >= recipes.Count)
            {
                Console.WriteLine("Invalid recipe.");
                Console.ReadKey();
                return;
            }

            ShowRecipeMenu(recipes[index]);
        }

        private void ShowRecipeMenu(Recipe recipe)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine(recipe.Name);
                Console.WriteLine();

                Console.WriteLine("1. View");
                Console.WriteLine("2. Delete");
                Console.WriteLine("3. Add to favorites");
                Console.WriteLine("4. Shopping list");
                Console.WriteLine("5. Scale portions");
                Console.WriteLine("6. Edit recipe");
                Console.WriteLine("0. Back");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowRecipe(recipe);
                        break;

                    case "2":
                        DeleteRecipe(recipe);
                        isRunning = false;
                        break;

                    case "3":
                        recipe.IsFavorite = true;
                        _recipeService.UpdateRecipe(recipe);

                        Console.WriteLine("Recipe added to favorites.");
                        Console.ReadKey();
                        break;

                    case "4":
                        ShowShoppingList(recipe);
                        break;

                    case "5":
                        ScaleRecipe(recipe);
                        break;

                    case "6":
                        EditRecipe(recipe);
                        break;

                    case "0":
                        isRunning = false;
                        break;
                }
            }
        }

        private void ShowRecipe(Recipe recipe)
        {
            Console.Clear();

            Console.WriteLine($"Name: {recipe.Name}");
            Console.WriteLine($"Category: {recipe.Category}");
            Console.WriteLine($"Difficulty: {recipe.DifficultyLevel}");
            Console.WriteLine($"Cooking time: {recipe.CookingTimeInMinutes}");
            Console.WriteLine($"Portions: {recipe.Portions}");

            Console.WriteLine();
            Console.WriteLine("Ingredients:");

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Name} - {ingredient.Amount} {ingredient.UnitType}");
            }

            Console.WriteLine();
            Console.WriteLine("Steps:");

            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipe.Steps[i]}");
            }

            Console.WriteLine();
            Console.WriteLine($"Notes: {recipe.Notes}");

            Console.ReadKey();
        }

        private void AddRecipe()
        {
            Console.Clear();

            Recipe recipe = new Recipe();

            Console.Write("Recipe name: ");
            recipe.Name = Console.ReadLine();

            Console.Write("Cooking time in minutes: ");
            recipe.CookingTimeInMinutes = int.Parse(Console.ReadLine());

            Console.Write("Portions: ");
            recipe.Portions = int.Parse(Console.ReadLine());

            Console.WriteLine("Select category:");
            Console.WriteLine("0. Breakfast");
            Console.WriteLine("1. MainCourse");
            Console.WriteLine("2. Dessert");
            Console.WriteLine("3. Salad");
            Console.WriteLine("4. Soup");
            Console.WriteLine("5. Snack");

            recipe.Category = (RecipeCategory)int.Parse(Console.ReadLine());

            Console.WriteLine("Select difficulty:");
            Console.WriteLine("0. Easy");
            Console.WriteLine("1. Medium");
            Console.WriteLine("2. Hard");

            recipe.DifficultyLevel = (DifficultyLevel)int.Parse(Console.ReadLine());

            recipe.Ingredients = new List<Ingredient>();

            bool addMoreIngredients = true;

            while (addMoreIngredients)
            {
                Ingredient ingredient = new Ingredient();

                Console.Write("Ingredient name: ");
                ingredient.Name = Console.ReadLine();

                Console.Write("Amount: ");
                ingredient.Amount = double.Parse(Console.ReadLine());

                Console.WriteLine("Unit type:");
                Console.WriteLine("0. Grams");
                Console.WriteLine("1. Kilograms");
                Console.WriteLine("2. Milliliters");
                Console.WriteLine("3. Liters");
                Console.WriteLine("4. Pieces");

                ingredient.UnitType = (UnitType)int.Parse(Console.ReadLine());

                recipe.Ingredients.Add(ingredient);

                Console.Write("Add another ingredient? (y/n): ");

                string answer = Console.ReadLine();

                if (answer.ToLower() != "y")
                {
                    addMoreIngredients = false;
                }
            }

            recipe.Steps = new List<string>();

            bool addMoreSteps = true;

            while (addMoreSteps)
            {
                Console.Write("Step: ");

                string step = Console.ReadLine();

                recipe.Steps.Add(step);

                Console.Write("Add another step? (y/n): ");

                string answer = Console.ReadLine();

                if (answer.ToLower() != "y")
                {
                    addMoreSteps = false;
                }
            }

            Console.Write("Notes: ");
            recipe.Notes = Console.ReadLine();

            recipe.IsFavorite = false;

            _recipeService.AddRecipe(recipe);

            Console.WriteLine("Recipe added.");

            Console.ReadKey();
        }

        private void SearchRecipes()
        {
            Console.Clear();

            Console.Write("Enter recipe name: ");

            string name = Console.ReadLine();

            List<Recipe> recipes = _recipeService.SearchByName(name);

            ShowRecipes(recipes);
        }

        private void FilterRecipes()
        {
            Console.Clear();

            Console.WriteLine("0. Breakfast");
            Console.WriteLine("1. MainCourse");
            Console.WriteLine("2. Dessert");
            Console.WriteLine("3. Salad");
            Console.WriteLine("4. Soup");
            Console.WriteLine("5. Snack");

            RecipeCategory category =
                (RecipeCategory)int.Parse(Console.ReadLine());

            List<Recipe> recipes =
                _recipeService.FilterByCategory(category);

            ShowRecipes(recipes);
        }

        private void SortRecipes()
        {
            Console.Clear();

            Console.WriteLine("0. By name");
            Console.WriteLine("1. By cooking time");
            Console.WriteLine("2. By ingredient count");

            SortOption sortOption =
                (SortOption)int.Parse(Console.ReadLine());

            List<Recipe> recipes =
                _recipeService.SortRecipes(sortOption);

            ShowRecipes(recipes);
        }

        private void ShowFavorites()
        {
            List<Recipe> allRecipes = _recipeService.GetAllRecipes();

            List<Recipe> favoriteRecipes = new List<Recipe>();

            foreach (Recipe recipe in allRecipes)
            {
                if (recipe.IsFavorite)
                {
                    favoriteRecipes.Add(recipe);
                }
            }

            ShowRecipes(favoriteRecipes);
        }

        private void DeleteRecipe(Recipe recipe)
        {
            List<Recipe> recipes = _recipeService.GetAllRecipes();

            int index = recipes.IndexOf(recipe);

            _recipeService.DeleteRecipe(index);

            Console.WriteLine("Recipe deleted.");

            Console.ReadKey();
        }

        private void ShowShoppingList(Recipe recipe)
        {
            Console.Clear();

            Console.WriteLine("Select ingredients you already have.");
            Console.WriteLine();

            List<Ingredient> missingIngredients = new List<Ingredient>();

            for (int i = 0; i < recipe.Ingredients.Count; i++)
            {
                Ingredient ingredient = recipe.Ingredients[i];

                Console.Write($"Do you have {ingredient.Name}? (y/n): ");

                string answer = Console.ReadLine();

                if (answer.ToLower() == "n")
                {
                    missingIngredients.Add(ingredient);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Shopping list:");

            if (missingIngredients.Count == 0)
            {
                Console.WriteLine("You already have all ingredients.");
            }
            else
            {
                foreach (Ingredient ingredient in missingIngredients)
                {
                    Console.WriteLine($"{ingredient.Name} - {ingredient.Amount} {ingredient.UnitType}");
                }
            }

            Console.ReadKey();
        }

        private void ScaleRecipe(Recipe recipe)
        {
            Console.Clear();

            Console.Write("Enter new portions count: ");

            int newPortions = int.Parse(Console.ReadLine());

            double multiplier = (double)newPortions / recipe.Portions;

            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                ingredient.Amount = ingredient.Amount * multiplier;
            }

            recipe.Portions = newPortions;

            _recipeService.UpdateRecipe(recipe);

            Console.WriteLine("Recipe updated.");

            Console.ReadKey();
        }

        private void EditRecipe(Recipe recipe)
        {
            Console.Clear();

            Console.Write("New recipe name: ");
            recipe.Name = Console.ReadLine();

            Console.Write("New cooking time: ");
            recipe.CookingTimeInMinutes = int.Parse(Console.ReadLine());

            Console.Write("New notes: ");
            recipe.Notes = Console.ReadLine();

            _recipeService.UpdateRecipe(recipe);

            Console.WriteLine("Recipe updated.");

            Console.ReadKey();
        }

        private void ShowSettings()
        {
            Console.Clear();

            AppSettings settings = _recipeService.GetSettings();

            Console.WriteLine($"Default portions: {settings.DefaultPortions}");
            Console.WriteLine($"Default sort: {settings.DefaultSortOption}");

            Console.WriteLine();
            Console.Write("New default portions: ");

            settings.DefaultPortions =
                int.Parse(Console.ReadLine());

            _recipeService.SaveSettings(settings);

            Console.WriteLine("Settings saved.");

            Console.ReadKey();
        }
    }
}