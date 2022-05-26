using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
				public string Quantity { get; set; }
				public int RecipeId { get; set; }
    }
}