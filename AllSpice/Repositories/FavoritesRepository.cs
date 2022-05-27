using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
	public class FavoritesRepository
	{
		private readonly IDbConnection _db;

		public FavoritesRepository(IDbConnection db)
		{
			_db = db;
		}
		internal Favorite Get(int recipeId, string userId)
		{
			string sql = @"
			SELECT
				*
			FROM favorites f
			WHERE f.recipeId = @recipeId
			AND f.accountId = @userId;
			";
			return _db.QueryFirstOrDefault<Favorite>(sql, new { recipeId, userId });
		}
		internal List<Favorite> GetAccountFavorites(string userId)
		{
			string sql = @"
			SELECT
				r.*,
				f.*
			FROM favorites f
			JOIN recipes r ON f.recipeId = r.id
			WHERE f.accountId = @userId;
			";
			return _db.Query<Recipe, Favorite, Favorite>(sql, (recipe, favorite) =>
			{
				favorite.FavoriteRecipe = recipe;
				return favorite;
			}, new { userId }).ToList();
		}
		internal void Create(int recipeId, string userId)
		{
			string sql = @"
			INSERT INTO favorites
			(recipeId, accountId)
			VALUES
			(@RecipeId, @UserId);
			SELECT LAST_INSERT_ID();
			";
			_db.Execute(sql, new { recipeId, userId });
		}

		internal void Delete(int id)
		{
			string sql = @"DELETE FROM favorites WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}