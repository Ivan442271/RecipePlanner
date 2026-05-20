using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using RecipePlanner.Domain.Interfaces;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.Application.Repositories
{
    public class JsonRecipeRepository : IRepository<Recipe>
    {
        private const string FilePath = "recipes.json";

        public List<Recipe> GetAll()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Recipe>();
            }

            string json = File.ReadAllText(FilePath);

            List<Recipe> recipes = JsonSerializer.Deserialize<List<Recipe>>(json);

            if (recipes == null)
            {
                return new List<Recipe>();
            }

            return recipes;
        }

        public void SaveAll(List<Recipe> items)
        {
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FilePath, json);
        }
    }
}