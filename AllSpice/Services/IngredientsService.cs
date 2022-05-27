using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
	public class IngredientsService
	{
		private readonly IngredientsRepository _repo;

		public IngredientsService(IngredientsRepository repo)
		{
			_repo = repo;
		}

		internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
		{
			return _repo.GetIngredientsByRecipe(recipeId);
		}
		internal Ingredient Get(int id)
		{
			Ingredient ingredient = _repo.Get(id);
			if (ingredient == null)
			{
				throw new Exception("Invalid ingredient id.");
			}
			return ingredient;
		}
		internal Ingredient Create(Ingredient ingData, Recipe recipe)
		{
			if (recipe.CreatorId != ingData.CreatorId)
			{
				throw new Exception("You cannot add ingredients to a recipe that is not yours.");
			}
			return _repo.Create(ingData);
		}

		internal Ingredient Edit(Ingredient ingData)
		{
			Ingredient original = Get(ingData.Id);
			if (original.CreatorId != ingData.CreatorId)
			{
				throw new Exception("You cannot edit ingredients on a recipe that is not yours.");
			}

			original.Name = ingData.Name ?? original.Name;
			original.Quantity = ingData.Quantity ?? original.Quantity;

			_repo.Edit(original);

			return Get(original.Id);
		}

		internal void Delete(int id, string userId)
		{
			Ingredient target = Get(id);
			if (target.CreatorId != userId)
			{
				throw new Exception("You cannot delete ingredients on a recipe that is not yours.");
			}
			_repo.Delete(id);
		}
	}
}