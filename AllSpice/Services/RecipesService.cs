using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
	public class RecipesService
	{
		private readonly RecipesRepository _repo;

		public RecipesService(RecipesRepository repo)
		{
			_repo = repo;
		}

		internal List<Recipe> Get()
		{
			return _repo.Get();
		}
		internal Recipe Get(int id)
		{
			Recipe recipe = _repo.Get(id);
			if (recipe == null)
			{
				throw new Exception("Invalid recipe id");
			}
			return recipe;
		}
		internal Recipe Create(Recipe recipeData)
		{
			return _repo.Create(recipeData);
		}

		internal Recipe Edit(Recipe recipeData)
		{
			Recipe original = Get(recipeData.Id);
			if (original.CreatorId != recipeData.CreatorId)
			{
				throw new Exception("You cannot edit recipes that do not belong to you.")
			}
			original.Title = recipeData.Title ?? original.Title;
			original.Category = recipeData.Category ?? original.Category;
			original.Picture = recipeData.Picture ?? original.Picture;
			original.Subtitle = recipeData.Subtitle ?? original.Subtitle;
		}
	}
}