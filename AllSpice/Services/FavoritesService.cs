using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
	public class FavoritesService
	{
		private readonly FavoritesRepository _repo;

		public FavoritesService(FavoritesRepository repo)
		{
			_repo = repo;
		}
		internal Favorite Get(int recipeId, string userId)
		{
			Favorite favorite = _repo.Get(recipeId, userId);
			if (favorite == null)
			{
				throw new Exception("That recipe is not on your favorites list.");
			}
			return favorite;
		}

		internal void Create(int recipeId, string userId)
		{
			_repo.Create(recipeId, userId);
		}

		internal void Delete(int recipeId, string userId)
		{
			Favorite favorite = Get(recipeId, userId);
			if (favorite.AccountId != userId)
			{
				throw new Exception("That is not your favorite to remove!");
			}
			_repo.Delete(favorite.Id);
		}

		internal List<Favorite> GetByAccount(string userId)
		{
			return _repo.GetAccountFavorites(userId);
		}
	}
}