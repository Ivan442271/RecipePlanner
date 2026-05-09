using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Core.Enums;

namespace RecipePlanner.Core.Models;

public class Recipe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Category Category { get; set; }
    public Complexity Difficulty { get; set; }
    public int CookingTimeInMinutes { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    public List<string> Steps { get; set; }
    public bool IsFavorite { get; set; }
    public string Notes { get; set; }
}