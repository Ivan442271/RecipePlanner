using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Core.Enums;

namespace RecipePlanner.Core.Models;

public class Ingredient
{
    public string Name { get; set; }
    public double Amount { get; set; }
    public Unit Unit { get; set; }
}
