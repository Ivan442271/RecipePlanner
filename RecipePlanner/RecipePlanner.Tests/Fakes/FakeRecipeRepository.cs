using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Domain.Interfaces;
using RecipePlanner.Domain.Models;

namespace RecipePlanner.Tests.Fakes
{
    public class FakeRecipeRepository : IRepository<Recipe>
    {
        private List<Recipe> _recipes;

        public FakeRecipeRepository()
        {
            _recipes = new List<Recipe>();
        }

        public List<Recipe> GetAll()
        {
            return _recipes;
        }

        public void SaveAll(List<Recipe> items)
        {
            _recipes = items;
        }
    }
}