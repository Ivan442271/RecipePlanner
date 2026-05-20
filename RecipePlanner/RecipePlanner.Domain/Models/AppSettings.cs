using System;
using System.Collections.Generic;
using System.Text;
using RecipePlanner.Domain.Enums;

namespace RecipePlanner.Domain.Models
{
    public class AppSettings
    {
        public int DefaultPortions { get; set; }

        public SortOption DefaultSortOption { get; set; }
    }
}
