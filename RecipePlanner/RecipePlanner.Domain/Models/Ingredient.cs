using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Domain.Enums;

namespace RecipePlanner.Domain.Models
{
    public class Ingredient
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public UnitType UnitType { get; set; }
    }
}
