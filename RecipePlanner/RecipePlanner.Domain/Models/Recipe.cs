using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Domain.Enums;

namespace RecipePlanner.Domain.Models
{
    public class Recipe
    {
        public string Name { get; set; }

        public RecipeCategory Category { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public int CookingTimeInMinutes { get; set; }

        public int Portions { get; set; }

        public bool IsFavorite { get; set; }

        public string Notes { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<string> Steps { get; set; }
    }
}
